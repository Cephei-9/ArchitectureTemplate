using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace DefaultNamespace
{
    public class InputCheck : MonoBehaviour
    {
        private void Update()
        {
            float horizontalAxis = Input.GetAxis("Horizontal");
            bool isSpasePressed = Input.GetKey(KeyCode.Space);

            Debug.Log($"Horizontal: {horizontalAxis}");
            Debug.Log($"Space: {isSpasePressed}");
        }

        public async UniTask<bool> GetBoolValueAsync()
        {
            await UniTask.WaitForSeconds(1);
            return true;
        }
    }
}