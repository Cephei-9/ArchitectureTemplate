using UnityEngine;

namespace DefaultNamespace.GameField
{
    public class GameFieldTester : MonoBehaviour
    {
        private Vector3 _raycastPosition;
        private MouseRaycaster _mouseRaycaster;

        private void Start()
        {
            _mouseRaycaster = new MouseRaycaster(Camera.main);
        }

        private void Update()
        {
            _mouseRaycaster.RaycastToGround(out _raycastPosition);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(_raycastPosition, 0.3f);
        }
    }
}