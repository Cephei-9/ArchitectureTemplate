using UnityEngine;

namespace ArchitectureTemplate.UI
{
    /// <summary>
    /// ScriptableObject holding window-to-asset mapping for WindowFactory.
    /// </summary>
    [CreateAssetMenu(menuName = "UI/Window Links")]
    public class WindowLinks : ScriptableObject
    {
        [SerializeField] private WindowPrefabLink[] _links;

        public WindowPrefabLink[] Links => _links;
    }
}
