using UniRx;
using UnityEngine;

namespace DefaultNamespace.FieldObjects
{
    public class FieldObjectTransparentView : MonoBehaviour
    {
        private static readonly int BaseColorId = Shader.PropertyToID("_BaseColor");
        private static readonly int ColorId = Shader.PropertyToID("_Color");
        
        [SerializeField] private FieldObjectModel _model;
        [SerializeField] private MeshRenderer _renderer;
        
        [SerializeField] private Material _defaultMaterial;
        [SerializeField] private Material _transparentMaterial;
        
        [SerializeField] private Color _allowedColor = Color.green;
        [SerializeField] private Color _prohibitedColor = Color.red;

        private Material _currentMaterial;
        private MaterialPropertyBlock _colorMaterialPropertyBlock;

        private void Awake()
        {
            _colorMaterialPropertyBlock = new MaterialPropertyBlock();
            _currentMaterial = _defaultMaterial;
        }

        private void Start()
        {
            _model.Status.Subscribe(OnStatusChangeHandler).AddTo(this);
        }

        private void OnStatusChangeHandler(FieldObjectStatus newStatus)
        {
            if (newStatus == FieldObjectStatus.MovingAllowed)
                SetTransparentMaterial(_allowedColor);
            else if (newStatus == FieldObjectStatus.MovingProhibited)
                SetTransparentMaterial(_prohibitedColor);
            else if (newStatus == FieldObjectStatus.Stay)
                SetDefaultMaterial();
        }

        private void SetTransparentMaterial(Color color)
        {
            SetMaterial(_transparentMaterial);
            
            _colorMaterialPropertyBlock.Clear();
            _colorMaterialPropertyBlock.SetColor(BaseColorId, color);
            _colorMaterialPropertyBlock.SetColor(ColorId, color);
            _renderer.SetPropertyBlock(_colorMaterialPropertyBlock);
        }

        private void SetDefaultMaterial()
        {
            SetMaterial(_defaultMaterial);
            
            _renderer.SetPropertyBlock(null);
        }
        
        private void SetMaterial(Material newMaterial)
        {
            if (_currentMaterial == newMaterial) 
                return;
            
            _renderer.sharedMaterial = newMaterial;
            _currentMaterial = newMaterial;
        }
    }
}