using UnityEngine;

namespace DefaultNamespace.GameField
{
    public class MouseRaycaster
    {
        private readonly int _groundLayerMask;
        private readonly Camera _camera;

        public MouseRaycaster(Camera camera)
        {
            _camera = camera;
            _groundLayerMask = LayerMask.GetMask(PhysicsLayers.Ground);
        }

        public bool RaycastToGround(out Vector3 worldPoint)
        {
            worldPoint = default;

            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _groundLayerMask))
                return false;

            worldPoint = hit.point;
            return true;
        }
    }
}