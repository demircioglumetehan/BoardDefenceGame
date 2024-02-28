using System.Collections.Generic;
using BoardDefenceGame.Enemy;
using UnityEngine;
namespace BoardDefenceGame.UI.Panel
{
    public class KilledEnemiesInfoPanel : MonoBehaviour
    {
        #region Fields
        [Header("Child Injections")]
        [SerializeField] private List<KilledEnemyCell> killedEnemyCells;
        #endregion

        #region Unity Methods
        private void OnEnable()
        {
            SetKilledEnemyCells();
        }
        #endregion

        #region Private Methods
        private void SetKilledEnemyCells()
        {
            var totalKilledEnemies = EnemySpawner.Instance.KilledEnemies;
            killedEnemyCells.ForEach(i => i.gameObject.SetActive(false));
            for (int i = 0; i < totalKilledEnemies.Count; i++)
            {
                killedEnemyCells[i].gameObject.SetActive(true);
                killedEnemyCells[i].Initialize(totalKilledEnemies[i]);
            }
        }
        #endregion

    }
}
