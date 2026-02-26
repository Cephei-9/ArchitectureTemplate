using DefaultNamespace.GameField.FieldCell;
using UnityEngine;

namespace DefaultNamespace.GameField
{
    public class GameFieldService
    {
        private static readonly Vector3 StartFieldWorldPosition = Vector3.zero;
        private static readonly Vector3Int FieldSizeConst = new (10, 10);
        private static readonly float CellSize = 1;

        public Vector3Int FieldSize => FieldSizeConst;
        public FieldCellEntity[,] FieldCellsEntities;

        public GameFieldService()
        {
            
        }
    }
}