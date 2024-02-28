using System.Collections.Generic;
using System.Linq;
using BoardDefenceGame.Events;
using UnityEngine;

namespace BoardDefenceGame.UI.Panel.Controllers
{
    public class DefenceItemSpawnButtonsController : MonoBehaviour
    {
        #region Fields
        [SerializeField] private SpawnDefenceItemButton spawnDefenceItemButtonPrefab;
        private List<SpawnDefenceItemButton> spawnDefenceItemButtons;
        #endregion

        #region Unity Methods
        private void OnEnable()
        {
            GameEventsUI.OnRemainingDefenceItemsSet += InitiateSpawnDefenceItemButtons;
        }

        private void OnDisable()
        {
            GameEventsUI.OnRemainingDefenceItemsSet -= InitiateSpawnDefenceItemButtons;

        }
        private void Awake()
        {
            spawnDefenceItemButtons = GetComponentsInChildren<SpawnDefenceItemButton>(true).ToList();
        }
        #endregion

        #region Private Methods
        private void InitiateSpawnDefenceItemButtons(List<LevelBasedDefenceItem> remainingItems)
        {
            CheckForButtonNumbers(remainingItems);
            for (int i = 0; i < remainingItems.Count; i++)
            {
                spawnDefenceItemButtons[i].Initialize(remainingItems[i]);
            }
        }

        private void CheckForButtonNumbers(List<LevelBasedDefenceItem> remainingItems)
        {
            if (spawnDefenceItemButtons.Count < remainingItems.Count)
            {
                var totalCreatingButtonNumber = remainingItems.Count - spawnDefenceItemButtons.Count;
                for (int i = 0; i < totalCreatingButtonNumber; i++)
                {
                    var newSpawnItemButton = Instantiate(spawnDefenceItemButtonPrefab, spawnDefenceItemButtons[0].transform.parent);
                    spawnDefenceItemButtons.Add(newSpawnItemButton);
                }
            }
        }
        #endregion

    }

}
