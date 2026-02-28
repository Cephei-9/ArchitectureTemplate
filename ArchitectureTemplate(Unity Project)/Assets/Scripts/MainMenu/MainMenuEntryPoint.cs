using ArchitectureTemplate.UI;
using UnityEngine;
using Zenject;

namespace ArchitectureTemplate.MainMenu
{
    public class MainMenuEntryPoint : MonoBehaviour
    {
        private WindowService _windowService;
        
        [Inject]
        public void Construct(WindowService windowService)
        {
            _windowService = windowService;
        }
        
        public void EnterMainMenu()
        {
            Debug.Log("[MainMenuStart] Main menu started.");

            _windowService.CloseWindow<InitializationScreenPresenter>();
        }
    }
}
