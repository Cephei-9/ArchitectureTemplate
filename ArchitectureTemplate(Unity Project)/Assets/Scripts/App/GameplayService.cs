using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using SceneLoading;
using UniRx;
using UnityEngine;

namespace Game.App
{
    public sealed class GameplayService
    {
        private readonly SceneLoader _sceneLoader;
        private readonly ReactiveProperty<bool> _isBusy = new(false);

        public IReadOnlyReactiveProperty<bool> IsBusy => _isBusy;

        public GameplayService(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        public async UniTask<bool> ReturnToMainMenuAsync(CancellationToken cancellationToken = default)
        {
            if(_isBusy.Value)
                return false;
            
            Debug.Log("[GameplayService] ReturnToMainMenuAsync started.");
            
            _isBusy.Value = true;

            bool isSuccess = await _sceneLoader.LoadSceneAsync(SceneIds.MainMenu, cancellationToken);
            
            _isBusy.Value = false;
                
            if (isSuccess)
            {
                Debug.Log("[GameplayService] ReturnToMainMenuAsync succeeded.");
                return true;
            }
                
            Debug.Log("[GameplayService] ReturnToMainMenuAsync failed");
            return false;
        }
    }
}
