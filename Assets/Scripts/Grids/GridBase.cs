using UnityEngine;

namespace BoardDefenceGame.Core.Grid
{
    public abstract class GridBase : MonoBehaviour
    {
        #region Fields
        public bool IsEnemySpawnerGrid { get; private set; } = false;
        public Vector2Int BoardPosition { get; private set; }
        #endregion

        #region Public Virtual Methods
        public virtual void Initialize(int x, int y)
        {
            BoardPosition = new Vector2Int(x, y);
        }
        public virtual void SetAsEnemySpawnerGrid()
        {
            IsEnemySpawnerGrid = true;
        }
        #endregion

    }
}
