using UnityEngine;

namespace ArchitectureTemplate.DI
{
    /// <summary>
    /// Abstraction over a DI container that provides basic resolve and instantiation operations.
    /// </summary>
    public interface IDiContainer
    {
        T Resolve<T>();

        GameObject InstantiatePrefab(GameObject prefab);

        void InjectGameObject(GameObject gameObject);
    }
}

