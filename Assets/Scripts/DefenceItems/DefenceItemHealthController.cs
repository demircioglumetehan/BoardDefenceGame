using System;
using UnityEngine;

namespace BoardDefenceGame.DefenceItems
{
    public class DefenceItemHealthController : MonoBehaviour
    {
        public Action<float, float> OnCurrentHealthChanged;
        #region Fields
        private DefenceItem defenceItem;
        public bool IsDead { get; private set; }
        private float currentHealth = 10;
        private float maximumHealth = 10;
        #endregion
        #region Public Methods
        public void InitializeHealth(int health, DefenceItem defenceItem)
        {
            IsDead = false;
            this.defenceItem = defenceItem;
            currentHealth = health;
            maximumHealth = currentHealth;
        }

        public void TakeDamage(float enemyDamage)
        {
            if (currentHealth <= 0)
                return;
            currentHealth -= enemyDamage;
            OnCurrentHealthChanged?.Invoke(currentHealth, maximumHealth);
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                defenceItem?.DestroyDefenceItem();
                IsDead = true;
            }
        }
        #endregion

    }

}
