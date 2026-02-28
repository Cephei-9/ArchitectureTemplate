using UnityEngine;
using Zenject;

namespace ArchitectureTemplate.Gameplay
{
    public class GameplayStart : IInitializable
    {
        public void Initialize()
        {
            Debug.Log("[GameplayStart] Gameplay started.");
        }
    }
}
