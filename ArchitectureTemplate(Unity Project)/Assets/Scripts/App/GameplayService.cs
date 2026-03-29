using System.Threading;
using Cysharp.Threading.Tasks;
using UniRx;

namespace Game.App
{
    public sealed class GameplayService
    {
        private readonly ReturnToMainMenuOperation _returnToMainMenuOperation;
        private readonly ReactiveProperty<bool> _isBusy = new(false);

        public IReadOnlyReactiveProperty<bool> IsBusy => _isBusy;

        public GameplayService(ReturnToMainMenuOperation returnToMainMenuOperation)
        {
            _returnToMainMenuOperation = returnToMainMenuOperation;
        }

        public async UniTask<bool> ReturnToMainMenuAsync(CancellationToken cancellationToken = default)
        {
            if(_isBusy.Value)
                return false;
            
            _isBusy.Value = true;

            bool isSuccess = await _returnToMainMenuOperation.ExecuteAsync(cancellationToken);
            
            _isBusy.Value = false;
            return isSuccess;
        }
    }
}
