using System;
using System.Threading;
using ArchitectureTemplate.AssetManagement;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace ArchitectureTemplate.UI
{
    public class WindowFactory
    {
        private readonly DiContainer _container;
        private readonly IAssetService _assetService;

        public WindowFactory(DiContainer container, IAssetService assetService)
        {
            _container = container;
            _assetService = assetService;
        }

        public WindowHandle Create<TPresentationModel>(out TPresentationModel presentationModel)
            where TPresentationModel : IDefaultPresentationModel
        {
            CancellationTokenSource cts = new();
            
            presentationModel = _container.Resolve<TPresentationModel>();
            presentationModel.Initialize(cts.Token);
            
            return CreateInternal(presentationModel, cts);
        }

        public WindowHandle Create<TPresentationModel, TArgs>(TArgs args, out TPresentationModel presentationModel)
            where TPresentationModel : IArgumentedPresentationModel<TArgs>
        {
            CancellationTokenSource cts = new();
            
            presentationModel = _container.Resolve<TPresentationModel>();
            presentationModel.InitializeArgument(args, cts.Token);

            return CreateInternal(presentationModel, cts);
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
