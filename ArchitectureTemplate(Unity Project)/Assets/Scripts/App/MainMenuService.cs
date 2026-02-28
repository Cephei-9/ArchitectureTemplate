using System.Threading;
using Cysharp.Threading.Tasks;
using SceneLoading;
using UniRx;
using UnityEngine;

namespace Game.App
{
    public sealed class MainMenuService
    {
        private readonly SceneLoader _sceneLoader;
        private readonly ReactiveProperty<bool> _isBusy = new(false);

        public IReadOnlyReactiveProperty<bool> IsBusy => _isBusy;

        public MainMenuService(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        public async UniTask<bool> StartGameAsync(CancellationToken cancellationToken = default)
        {
            if(_isBusy.Value)
                return false;

            Debug.Log("[MainMenuService] StartGameAsync started.");
            
            _isBusy.Value = true;

            bool isSuccess = await _sceneLoader.LoadSceneAsync(SceneIds.Gameplay, cancellationToken);

            _isBusy.Value = false;

            if (isSuccess)
            {
                Debug.Log("[MainMenuService] StartGameAsync succeeded.");
                return true;
            }

            Debug.Log("[MainMenuService] StartGameAsync failed");
            return false;
        }

        public bool QuitGameAsync()
        {
            if (_isBusy.Value)
                return false;
            
            Application.Quit();
            
            Debug.Log("[MainMenuService] QuitGameAsync finished.");
            return false;
        }
    }
}
