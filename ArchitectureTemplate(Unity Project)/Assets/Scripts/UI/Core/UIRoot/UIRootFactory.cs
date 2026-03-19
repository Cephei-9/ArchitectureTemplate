using ArchitectureTemplate.AssetManagement;
using UnityEngine;
using Zenject;

namespace ArchitectureTemplate.UI
{
    /// <summary>
    /// Factory that creates UIRoot from assets and marks it as DontDestroyOnLoad.
    /// Implements IFactory&lt;UIRoot&gt; directly so it works with Bind().FromFactory&lt;UIRootFactory&gt;()
    /// (PlaceholderFactory requires IProvider/InjectContext that are not available in that binding path).
    /// </summary>
    public class UIRootFactory : IFactory<UIRoot>
    {
        private readonly IAssetService _assetService;

        public UIRootFactory(IAssetService assetService)
        {
            _assetService = assetService;
        }

        public UIRoot Create()
        {
            // GameObject prefab = _assetService.GetAsset<GameObject>(AssetKey.UIRoot);
            // GameObject instance = Object.Instantiate(prefab);
            //
            // Object.DontDestroyOnLoad(instance);
            //
            // UIRoot uiRoot = instance.GetComponent<UIRoot>();
            // return uiRoot;

            return null;
        }
    }
}

