using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace ArchitectureTemplate.UI
{
    /// <summary>
    /// Instantiates window prefabs via Zenject and wires view with presentation model.
    /// </summary>
    public class WindowFactory
    {
        private readonly DiContainer _container;
        private readonly IAssetProvider _assets;
        private readonly Dictionary<WindowId, AssetId> _windowToAssetMap;

        public WindowFactory(
            DiContainer container,
            IAssetProvider assets,
            IEnumerable<WindowPrefabLink> links)
        {
            _container = container;
            _assets = assets;
            _windowToAssetMap = new Dictionary<WindowId, AssetId>();

            foreach (WindowPrefabLink link in links)
                _windowToAssetMap[link.WindowId] = link.AssetId;
        }

        public WindowHandle Create(WindowId id, object args)
        {
            if (!_windowToAssetMap.TryGetValue(id, out AssetId assetId))
                throw new InvalidOperationException($"No prefab mapping for window: {id}");

            GameObject prefab = _assets.LoadPrefab<GameObject>(assetId);
            GameObject instance = _container.InstantiatePrefab(prefab);
            _container.InjectGameObject(instance);

            IWindowView view = instance.GetComponentInChildren<IWindowView>(true);

            if (view == null)
            {
                Object.Destroy(instance);
                throw new InvalidOperationException($"Prefab has no IWindowView: {id}");
            }

            view.PresentationModel.Initialize(args);
            view.Initialize(args);

            void Release()
            {
                view.Destroy();
            }

            return new WindowHandle(view, Release);
        }
    }
}
