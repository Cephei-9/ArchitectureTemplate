using ArchitectureTemplate.AssetManagement;
using UnityEngine;
using Zenject;

namespace ArchitectureTemplate.UI
{
    /// <summary>
    /// Factory that creates UIRoot from assets and marks it as DontDestroyOnLoad.
    /// </summary>
    public class UIRootFactory : PlaceholderFactory<UIRoot>
    {
        private readonly IAssetService _assetService;

        public UIRootFactory(IAssetService assetService)
        {
            _assetService = assetService;
        }

        public override UIRoot Create()
        {
            GameObject prefab = _assetService.GetAsset<GameObject>(AssetKey.UIRoot);
            GameObject instance = Object.Instantiate(prefab);

            Object.DontDestroyOnLoad(instance);

            UIRoot uiRoot = instance.GetComponent<UIRoot>();
            return uiRoot;
        }
    }
}

