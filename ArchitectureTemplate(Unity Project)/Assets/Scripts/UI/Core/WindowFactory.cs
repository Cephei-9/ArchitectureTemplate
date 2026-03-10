using System;
using System.Threading;
using ArchitectureTemplate.AssetManagement;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace ArchitectureTemplate.UI
{
    /// <summary>
    /// Instantiates window views via Zenject, resolves presenters via DI, and wires them together.
    /// </summary>
    public class WindowFactory
    {
        private readonly DiContainer _container;
        private readonly IAssetService _assetService;

        public WindowFactory(
            DiContainer container,
            IAssetService assetService)
        {
            _container = container;
            _assetService = assetService;
        }

        /// <summary>
        /// Creates a window with a presenter that does not require arguments.
        /// </summary>
        public WindowHandle Create<TPresentationModel>(out TPresentationModel presenter)
            where TPresentationModel : IDefaultPresentationModel
        {
            CancellationTokenSource cts = new();
            
            presenter = _container.Resolve<TPresentationModel>();
            presenter.Initialize(cts.Token);
            
            return CreateInternal(presenter, cts);
        }

        /// <summary>
        /// Creates a window with an argumented presenter.
        /// </summary>
        public WindowHandle Create<TPresentationModel, TArgs>(TArgs args, out TPresentationModel presenter)
            where TPresentationModel : IArgumentedPresentationModel<TArgs>
        {
            CancellationTokenSource cts = new();
            
            presenter = _container.Resolve<TPresentationModel>();
            presenter.InitializeArgument(args, cts.Token);

            return CreateInternal(presenter, cts);
        }

        private WindowHandle CreateInternal<TPresentationModel>(TPresentationModel presentationModel,
            CancellationTokenSource cts)
            where TPresentationModel : IPresentationModel
        {
            GameObject prefab = _assetService.GetAsset<GameObject>(presentationModel.ViewAssetKey);
            GameObject instance = _container.InstantiatePrefab(prefab);
            
            _container.InjectGameObject(instance);

            ICreatableWindowView<TPresentationModel> view = instance.GetComponent<ICreatableWindowView<TPresentationModel>>();

            if (view == null)
            {
                Object.Destroy(instance);
                throw new InvalidOperationException($"[WindowFactory] Prefab has no IWindowView for asset: {presentationModel.ViewAssetKey}");
            }

            view.Initialize(presentationModel, cts.Token);

            WindowHandle windowHandle = new(view, cts, Release);
            return windowHandle;

            void Release()
            {
                view.Destroy();
                presentationModel.Dispose();
            }
        }
    }
}
