using System;
using System.Threading;
using ArchitectureTemplate.AssetManagement;
using ArchitectureTemplate.DI;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ArchitectureTemplate.UI
{
    /// <summary>
    /// Factory that creates window instances from prefabs, resolves presentation models via DI.
    /// </summary>
    public class WindowFactory
    {
        private readonly IDiContainer _container;
        private readonly AssetService _assetService;
        private readonly UIRoot _uiRoot;

        public WindowFactory(IDiContainer container, AssetService assetService, UIRoot uiRoot)
        {
            _container = container;
            _assetService = assetService;
            _uiRoot = uiRoot;
        }

        public WindowHandle Create<TPresentationModel>(UILayer layer, out TPresentationModel presentationModel)
            where TPresentationModel : IDefaultPresentationModel
        {
            CancellationTokenSource cts = new();
            
            presentationModel = _container.Resolve<TPresentationModel>();
            presentationModel.Initialize(cts.Token);
            
            return CreateInternal(presentationModel, layer, cts);
        }

        public WindowHandle Create<TPresentationModel, TArgs>(TArgs args, UILayer layer, out TPresentationModel presentationModel)
            where TPresentationModel : IArgumentedPresentationModel<TArgs>
        {
            CancellationTokenSource cts = new();
            
            presentationModel = _container.Resolve<TPresentationModel>();
            presentationModel.InitializeArgument(args, cts.Token);

            return CreateInternal(presentationModel, layer, cts);
        }

        private WindowHandle CreateInternal<TPresentationModel>(TPresentationModel presentationModel,
            UILayer layer, CancellationTokenSource cts)
            where TPresentationModel : IPresentationModel
        {
            GameObject prefab = _assetService.GetAsset<GameObject>(presentationModel.ViewAssetKey);
            GameObject instance = _container.InstantiatePrefab(prefab);

            _uiRoot.SetUIElement(instance, layer);

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
