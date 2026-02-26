using System;
using UniRx;
using UnityEngine;

namespace DefaultNamespace.FieldObjects
{
    public class FieldObjectModel : MonoBehaviour
    {
        public ReactiveProperty<FieldObjectStatus> Status = new();
    }
}