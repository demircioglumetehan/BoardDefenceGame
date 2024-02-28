using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using BoardDefenceGame.Events;

namespace BoardDefenceGame.UI
{
    public class SpawnDefenceItemButton : MonoBehaviour
    {
        #region Fields
        [SerializeField] private TextMeshProUGUI remainingAmountText;
        [SerializeField] private TextMeshProUGUI defenceItemNameText;
        [SerializeField] private Image defenceItemIconImage;
        private EventTrigger eventTrigger;
        private LevelBasedDefenceItem DefenceItemSpawnFeature;
        #endregion

        #region PublicMethods
        public void Initialize(LevelBasedDefenceItem levelBasedDefenceItem)
        {
            CacheComponents();
            DefenceItemSpawnFeature = levelBasedDefenceItem;
            InitiateButtonView();
        }

        public void OnDefenceItemDrag()
        {
            Debug.Log(DefenceItemSpawnFeature.ToString() + " dragged");
            GameEventsUI.OnDefenceItemStartedToDrag?.Invoke(DefenceItemSpawnFeature.SpawnableDefenceItemFeature, this);
        }

        public void ReduceDefenseItemSize()
        {
            DefenceItemSpawnFeature.DecrementTotalSpawnableDefenceItemAmount(1);
            UpdateButtonView();
        }
        #endregion

        #region Private Methods
        private void CacheComponents()
        {
            eventTrigger = GetComponent<EventTrigger>();
        }

        private void InitiateButtonView()
        {
            defenceItemNameText.text = DefenceItemSpawnFeature.SpawnableDefenceItemFeature.ItemName;
            defenceItemIconImage.sprite = DefenceItemSpawnFeature.SpawnableDefenceItemFeature.DefenceItemSprite;
            UpdateButtonView();
        }

        private void UpdateButtonView()
        {
            eventTrigger.enabled = DefenceItemSpawnFeature.TotalSpawnableDefenceItemAmount > 0;
            remainingAmountText.text = DefenceItemSpawnFeature.TotalSpawnableDefenceItemAmount.ToString();
        }
        #endregion

    }
}
