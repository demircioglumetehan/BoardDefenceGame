using System.Collections.Generic;
using BoardDefenceGame.Events;
using UnityEngine;

namespace BoardDefenceGame.Core
{
    public class DefenceItemManager : MonoBehaviour
    {
        #region Fields
        private List<LevelBasedDefenceItem> UnplacedDefenceItems;
        #endregion

        #region Public Methods
        public void SetInitialDefenceItems(LevelDataScriptableObject currentLevelData)
        {
            UnplacedDefenceItems = new List<LevelBasedDefenceItem>();
            foreach (var defenceItem in currentLevelData.DefenceItems)
            {
                var defenceItemCopy = new LevelBasedDefenceItem(defenceItem.TotalSpawnableDefenceItemAmount, defenceItem.SpawnableDefenceItemFeature);
                UnplacedDefenceItems.Add(defenceItemCopy);
            }

            GameEventsUI.OnRemainingDefenceItemsSet?.Invoke(UnplacedDefenceItems);
        }
        #endregion

    }
}
