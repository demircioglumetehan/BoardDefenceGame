using System.Collections.Generic;
using BoardDefenceGame.Events;
using UnityEngine;
namespace BoardDefenceGame.UI.Panel
{
    public class PlayerBaseHealthPanel : MonoBehaviour
    {
        #region Fields
        [Header("Child Injections")]
        [SerializeField] private List<PlayerHealthCell> playerHealthCells;
        #endregion

        #region Unity Methods
        private void OnEnable()
        {
            GameEventsUI.OnPlayerHealthInitialized += InitiatePlayerHealthCells;
            GameEventsUI.OnPlayerHealthChanged += UpdatePlayerHealthCells;
        }
        private void OnDisable()
        {
            GameEventsUI.OnPlayerHealthInitialized -= InitiatePlayerHealthCells;
            GameEventsUI.OnPlayerHealthChanged -= UpdatePlayerHealthCells;
        }
        #endregion

        #region Private Methods
        private void InitiatePlayerHealthCells(int totalHealth)
        {
            playerHealthCells.ForEach(i => i.gameObject.SetActive(false));
            for (int i = 0; i < totalHealth; i++)
            {
                playerHealthCells[i].gameObject.SetActive(true);
                playerHealthCells[i].MakeEnable();

            }
        }

        private void UpdatePlayerHealthCells(int currentHealth)
        {
            playerHealthCells[currentHealth].MakeDisable();
        }
        #endregion

    }
}
