using System.Collections.Generic;
using BoardDefenceGame.Core.Grid;
using UnityEngine;

namespace BoardDefenceGame.Core
{
    public class BoardManager : SingletonBase<BoardManager>
    {
        #region Fields
        [SerializeField] private GridBase normalGrid;
        [SerializeField] private GridBase defenceGrid;
        [SerializeField] private float gridSize;
        [SerializeField] private Transform boardParent;

        private GridBase[,] gridMatrix;
        private List<GridBase> allGrids;
        private List<GridBase> enemySpawnerGrids;
        #endregion

        #region Public Methods
        public void CreateBoard(LevelDataScriptableObject currentLevelData)
        {
            allGrids = new List<GridBase>();
            enemySpawnerGrids = new List<GridBase>();

            var boardSize = currentLevelData.BoardDimensions;
            gridMatrix = new GridBase[boardSize.x, boardSize.y];
            var boardHalfSize = boardSize.y / 2;
            for (int i = 0; i < boardSize.x; i++)
            {
                for (int j = 0; j < boardSize.y; j++)
                {
                    GridBase createdGrid;
                    if (boardHalfSize > j)
                    {
                        createdGrid = Instantiate(defenceGrid, gridSize * new Vector3(i, j, 0f), Quaternion.identity);
                    }
                    else
                    {
                        createdGrid = Instantiate(normalGrid, gridSize * new Vector3(i, j, 0f), Quaternion.identity);
                        if (j == boardSize.y - 1)
                        {
                            createdGrid.SetAsEnemySpawnerGrid();
                            enemySpawnerGrids.Add(createdGrid);
                        }

                    }

                    createdGrid.transform.SetParent(this.boardParent);
                    createdGrid.Initialize(i, j);
                    allGrids.Add(createdGrid);
                    gridMatrix[i, j] = createdGrid;
                    createdGrid.name = " " + i + "X" + j + " " + createdGrid.name;
                }
            }

        }

        public GridBase GetRandomEnemySpawnerGrid()
        {
            return enemySpawnerGrids[UnityEngine.Random.Range(0, enemySpawnerGrids.Count)];
        }
        public GridBase GetBelowGrid(GridBase upperGrid)
        {
            if (upperGrid.BoardPosition.y == 0)
                return null;
            var belowGrid = gridMatrix[upperGrid.BoardPosition.x, upperGrid.BoardPosition.y - 1];
            return belowGrid;
        }

        public List<GridBase> GetAllDirectionGrids(DefenseGrid centerGrid, int rangeInBlocks)
        {
            List<GridBase> grids = new List<GridBase>();
            int minimumX = Mathf.Max(0, centerGrid.BoardPosition.x - rangeInBlocks);
            int maximumX = Mathf.Min(gridMatrix.GetLength(0) - 1, centerGrid.BoardPosition.x + rangeInBlocks);
            int minimumY = Mathf.Max(0, centerGrid.BoardPosition.y - rangeInBlocks);
            int maximumY = Mathf.Min(gridMatrix.GetLength(1) - 1, centerGrid.BoardPosition.y + rangeInBlocks);
            for (int i = minimumX; i <= maximumX; i++)
            {
                for (int j = minimumY; j <= maximumY; j++)
                {
                    grids.Add(gridMatrix[i, j]);
                }
            }
            return grids;
        }

        public List<GridBase> GetForwardGrids(DefenseGrid standingGrid, int rangeInBlocks)
        {
            List<GridBase> grids = new List<GridBase>();
            int maximumY = Mathf.Min(gridMatrix.GetLength(1) - 1, standingGrid.BoardPosition.y + rangeInBlocks);
            for (int i = standingGrid.BoardPosition.y; i <= maximumY; i++)
            {
                grids.Add(gridMatrix[standingGrid.BoardPosition.x, i]);
            }
            return grids;
        }
        #endregion

    }

}
