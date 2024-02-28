using BoardDefenceGame.Core.Grid;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Pool;

namespace BoardDefenceGame.DefenceItems
{
    public class DefenceItem : MonoBehaviour
    {
        #region Fields
        public DefenceItemHealthController DefenceItemHealthController { get; private set; }
        public DefenceItemAttackController DefenceItemAttackController { get; private set; }
        private DefenceItemFeatureScriptableObject itemFeature;
        private DefenseGrid standingGrid;
        private ObjectPool<DefenceItem> itemPool;
        #endregion

        #region Public Methods
        public void InitializeItem(DefenceItemFeatureScriptableObject draggingDefenceItemFeature)
        {
            CacheComponents();
            this.itemFeature = draggingDefenceItemFeature;
            DefenceItemAttackController.enabled = false;
            DefenceItemHealthController.InitializeHealth(itemFeature.Health, this);
        }

        public void GoBackToButtonView(Vector3 destinationPosition)
        {
            transform.DOMove(destinationPosition, .5f).OnComplete(() =>
            {
                DestroyDefenceItem();
            });
        }

        public void StartDefending(DefenseGrid defenseGrid)
        {
            standingGrid = defenseGrid;
            DefenceItemAttackController.enabled = true;
            DefenceItemAttackController.Initialize(itemFeature);
        }

        public void DestroyDefenceItem()
        {
            if (standingGrid)
                standingGrid.OnDefenceItemDestroyed();
            this.gameObject.SetActive(false);
        }


        #endregion

        #region Private Methods
        private void CacheComponents()
        {
            this.DefenceItemHealthController = GetComponent<DefenceItemHealthController>();
            this.DefenceItemAttackController = GetComponentInChildren<DefenceItemAttackController>();
        }
        #endregion
    }

}
