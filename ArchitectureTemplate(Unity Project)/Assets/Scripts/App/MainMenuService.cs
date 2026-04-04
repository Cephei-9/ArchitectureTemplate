using System.Threading;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace Game.App
{
    public sealed class MainMenuService
    {
        private readonly StartGameplayOperation _startGameplayOperation;
        
        private readonly ReactiveProperty<bool> _isBusy = new(false);

        public IReadOnlyReactiveProperty<bool> IsBusy => _isBusy;

        public MainMenuService(StartGameplayOperation startGameplayOperation)
        {
            _startGameplayOperation = startGameplayOperation;
        }

        public async UniTask<bool> StartGameAsync(CancellationToken cancellationToken = default)
        {
            if(_isBusy.Value)
                return false;

            _isBusy.Value = true;

            bool isSuccess = await _startGameplayOperation.StartGameplayAsync(cancellationToken);

            _isBusy.Value = false;
            return isSuccess;
        }

        public void QuitGameAsync()
        {
            if (_isBusy.Value) return;

            Application.Quit();
            
            Debug.Log("[MainMenuService] QuitGameAsync finished.");
        }
    }
}
