using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace ArchitectureTemplate.UI
{
    /// <summary>
    /// Base class for window views. Owns presentation model and ensures disposal on destroy.
    /// </summary>
    public abstract class WindowView<TPm> : MonoBehaviour, IWindowView
        where TPm : IWindowPresentationModel
    {
        private bool _initialized;

        [Inject]
        private void Construct(TPm pm)
        {
            Pm = pm;
        }

        public event Action OnClosedEvent;

        public TPm Pm { get; private set; }

        IWindowPresentationModel IWindowView.PresentationModel => Pm;

        public void Initialize(object args)
        {
            if (_initialized)
                return;

            _initialized = true;
            Bind(args);
        }

        protected abstract void Bind(object args);

        public virtual UniTask CloseAsync()
        {
            RaiseOnClosed();
            return UniTask.CompletedTask;
        }

        protected void RaiseOnClosed()
        {
            OnClosedEvent?.Invoke();
        }

        public void Destroy()
        {
            SafeDisposePm();
            Object.Destroy(gameObject);
        }

        private void OnDestroy()
        {
            SafeDisposePm();
        }

        private void SafeDisposePm()
        {
            if (Pm == null)
                return;

            try
            {
                Pm.Dispose();
            }
            finally
            {
                Pm = default;
            }
        }
    }
}
