using BoardDefenceGame.Enemy;
using BoardDefenceGame.Events;
using UnityEngine;

namespace BoardDefenceGame.Core
{
    public class PlayerBaseHealthManager : MonoBehaviour
    {
        #region Fields
        private int currentHealth;
        #endregion

        #region Unity Methods
        private void OnEnable()
        {
            GameEvents.OnEnemyAttackedToPlayerBase += DecrementHealth;
        }
        private void OnDisable()
        {
            GameEvents.OnEnemyAttackedToPlayerBase -= DecrementHealth;

        }
        #endregion

        #region Public Methods
        public void Initialize(LevelDataScriptableObject currentLevel)
        {
            currentHealth = currentLevel.PlayerBaseHealth;
            GameEventsUI.OnPlayerHealthInitialized?.Invoke(currentHealth);
        }
        #endregion

        #region Private Methods
        private void DecrementHealth(BaseEnemy enemy)
        {
            if (currentHealth <= 0)
                return;
            currentHealth--;
            GameEventsUI.OnPlayerHealthChanged?.Invoke(currentHealth);
            if (currentHealth <= 0)
            {
                GameEvents.OnPlayerBaseDestroyed?.Invoke();
            }
        }
        #endregion

    }

}
