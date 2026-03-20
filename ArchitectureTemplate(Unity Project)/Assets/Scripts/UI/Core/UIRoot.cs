using System;
using ArchitectureTemplate.AssetManagement;
using UnityEngine;

namespace ArchitectureTemplate.UI
{
    /// <summary>
    /// Root object for UI hierarchy. Holds separate layers for screens, windows and popups.
    /// </summary>
    public class UIRoot : MonoBehaviour
    {
        [SerializeField] private Transform _screenLayer;
        [SerializeField] private Transform _windowsLayer;
        [SerializeField] private Transform _popupsLayer;

        public void SetUIElement(GameObject uiElement, UILayer layer)
        {
            Transform parent = GetLayerTransform(layer);
            uiElement.transform.SetParent(parent, false);
        }

        private Transform GetLayerTransform(UILayer layer)
        {
            return layer switch
            {
                UILayer.Screen => _screenLayer,
                UILayer.Windows => _windowsLayer,
                UILayer.Popups => _popupsLayer,
                _ => throw new ArgumentOutOfRangeException(nameof(layer), layer, null)
            };
        }
    }
}

