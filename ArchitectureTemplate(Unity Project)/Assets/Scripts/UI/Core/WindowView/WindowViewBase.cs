using System;
using System.Threading;
using UnityEngine;

namespace ArchitectureTemplate.UI
{
    /// <summary>
    /// Base class for window views. Owns presenter instance.
    /// </summary>
    public abstract class WindowViewBase<TPresentationModel> : MonoBehaviour, ICreatableWindowView<TPresentationModel> 
        where TPresentationModel : IPresentationModel
    {
        protected TPresentationModel PresentationModel;
        
        private CancellationToken _token;

        public event Action OnClosedEvent;
        
        public virtual void Initialize(TPresentationModel presentationModel, CancellationToken token)
        {
            PresentationModel = presentationModel;
            _token = token;
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }

        public abstract void Close();

        protected void InvokeOnClosedEvent()
        {
            OnClosedEvent?.Invoke();
        }
    }
}
