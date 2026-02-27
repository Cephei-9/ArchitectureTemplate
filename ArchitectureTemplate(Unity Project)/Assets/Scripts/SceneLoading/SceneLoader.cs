using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneLoading
{
    /// <summary>
    /// SceneLoader is a single entry point for scene loading.
    /// It blocks concurrent loads, exposes loading state and progress via UniRx, and returns success status.
    /// </summary>
    public sealed class SceneLoader
    {
        private readonly ReactiveProperty<bool> _isLoading = new(false);
        private readonly ReactiveProperty<float> _progress = new(0f);

        public IReadOnlyReactiveProperty<bool> IsLoading => _isLoading;
        public IReadOnlyReactiveProperty<float> Progress => _progress;

        public async UniTask<bool> LoadSceneAsync(string sceneId, CancellationToken cancellationToken = default)
        {
            if (_isLoading.Value)
                return false;

            Debug.Log("[SceneLoader] Start loading scene: " + sceneId);
            
            _isLoading.SetValueAndForceNotify(true);
            _progress.SetValueAndForceNotify(0f);
            
            try
            {
                bool isLoaded = await LoadInternalAsync(sceneId, cancellationToken);

                if (isLoaded)
                {
                    CompleteLoadingSuccessfully(sceneId);
                    return true;
                }

                CompleteLoadingWithFailure(sceneId);
                return false;
            }
            catch (Exception exception)
            {
                CompleteLoadingWithFailure(sceneId, exception);
                return false;
            }
            finally
            {
                _progress.SetValueAndForceNotify(1f);
                _isLoading.SetValueAndForceNotify(false);
            }
        }

        private void CompleteLoadingSuccessfully(string sceneId)
        {
            Debug.Log("[SceneLoader] Scene loaded successfully: " + sceneId);
        }

        private void CompleteLoadingWithFailure(string sceneId)
        {
            Debug.LogError($"[SceneLoader] Failed to load scene: {sceneId}");
        }

        private void CompleteLoadingWithFailure(string sceneId, Exception exception)
        {
            Debug.LogError($"[SceneLoader] Failed to load scene: {sceneId} exception: {exception}");
        }

        private async UniTask<bool> LoadInternalAsync(string sceneId, CancellationToken cancellationToken)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId, LoadSceneMode.Single);

            operation.allowSceneActivation = true;

            while (!operation.isDone)
            {
                cancellationToken.ThrowIfCancellationRequested();

                float progress = Mathf.Clamp01(operation.progress / 0.9f);
                _progress.Value = progress;

                await UniTask.Yield(PlayerLoopTiming.Update, cancellationToken);
            }

            _progress.Value = 1f;

            Scene loadedScene = SceneManager.GetSceneByName(sceneId);

            return loadedScene.IsValid() && loadedScene.isLoaded;
        }
    }
}
