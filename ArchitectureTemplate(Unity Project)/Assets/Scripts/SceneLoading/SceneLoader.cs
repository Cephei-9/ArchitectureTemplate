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

        public UniTask<bool> LoadEmptySceneAsync(CancellationToken cancellationToken = default)
        {
            return LoadSceneAsync(SceneIds.Empty, cancellationToken);
        }

        public async UniTask<bool> LoadSceneAsync(string sceneId, CancellationToken cancellationToken = default)
        {
            if (_isLoading.Value)
                return false;

            Debug.Log("[SceneLoader] Start loading scene: " + sceneId);
            
            _isLoading.SetValueAndForceNotify(true);
            _progress.SetValueAndForceNotify(0f);
            
            try
            {
                bool isLoaded = await LoadSceneAndTrackProgressAsync(sceneId, cancellationToken);

                if (isLoaded)
                {
                    Debug.Log("[SceneLoader] Scene loaded successfully: " + sceneId);
                    return true;
                }

                Debug.LogError($"[SceneLoader] Failed to load scene: {sceneId}");
                return false;
            }
            catch (Exception exception)
            {
                Debug.LogError($"[SceneLoader] Failed to load scene: {sceneId} exception: {exception}");
                return false;
            }
            finally
            {
                _progress.SetValueAndForceNotify(1f);
                _isLoading.SetValueAndForceNotify(false);
            }
        }

        private async UniTask<bool> LoadSceneAndTrackProgressAsync(string sceneId, CancellationToken cancellationToken)
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
