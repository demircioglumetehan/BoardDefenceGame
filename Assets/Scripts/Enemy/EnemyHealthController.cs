using System;
using UnityEngine;

namespace BoardDefenceGame.Enemy
{
    public class EnemyHealthController : MonoBehaviour
    {
        public Action<float, float> OnEnemyHealthChanged;

        #region Fields
        private BaseEnemy enemy;
        private float enemyHealth;
        #endregion

        #region Public Methods
        public void Initialize(BaseEnemy baseEnemy)
        {
            enemy = baseEnemy;
            this.enemyHealth = enemy.EnemyFeature.Health;
        }

        public void TakeDamage(int damageAmount)
        {
            if (enemyHealth <= 0)
                return;
            enemyHealth -= damageAmount;
            enemyHealth = Mathf.Max(0, enemyHealth);
            OnEnemyHealthChanged?.Invoke(enemyHealth, enemy.EnemyFeature.Health);
            if (enemyHealth <= 0)
            {
                enemy.DestroyEnemy();
            }
        }
        #endregion

    }

}
