using System;
using UnityEngine;
using Zenject;

namespace ArchitectureTemplate.DI
{
    /// <summary>
    /// Wrapper over Zenject.DiContainer that can switch between project container and current scene container.
    /// </summary>
    public class ZenjectDiContainer : IDiContainer
    {
        private DiContainer _currentContainer;

        public ZenjectDiContainer(DiContainer initialContainer)
        {
            _currentContainer = initialContainer;
        }

        public void UpdateContainer(DiContainer container)
        {
            _currentContainer = container ?? throw new ArgumentNullException(nameof(container));
        }

        public T Resolve<T>()
        {
            return _currentContainer.Resolve<T>();
        }

        public GameObject InstantiatePrefab(GameObject prefab)
        {
            return _currentContainer.InstantiatePrefab(prefab);
        }

        public void InjectGameObject(GameObject gameObject)
        {
            _currentContainer.InjectGameObject(gameObject);
        }
    }
}

