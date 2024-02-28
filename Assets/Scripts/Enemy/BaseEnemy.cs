using System;
using BoardDefenceGame.Core.Grid;
using BoardDefenceGame.Events;
using DG.Tweening;
using UnityEngine;

namespace BoardDefenceGame.Enemy
{
    public class BaseEnemy : MonoBehaviour
    {
        public Action OnEnemyDied;

        #region Fields
        public EnemyBehaviourController EnemyBehaviourController { get; private set; }
        public EnemyHealthController EnemyHealthController { get; private set; }
        public EnemyFeatureScriptableObject EnemyFeature { get; private set; }
        #endregion

        #region Public Methods
        public void Initialize(EnemyFeatureScriptableObject spawningEnemyFeature, GridBase spawningGrid)
        {
            CacheComponents();
            this.EnemyFeature = spawningEnemyFeature;
            EnemyBehaviourController?.Initialize(this, spawningGrid);
            EnemyHealthController?.Initialize(this);
        }

        public void TakeDamage(int damageAmount)
        {
            EnemyHealthController?.TakeDamage(damageAmount);
        }

        public void OnEnemyReachedPlayerBase()
        {
            GameEvents.OnEnemyAttackedToPlayerBase?.Invoke(this);
            transform.DOScale(Vector3.zero, 0.5f).OnComplete(() =>
            {
                DestroyEnemy();
            });
        }
        public void DestroyEnemy()
        {
            OnEnemyDied?.Invoke();
            GameEvents.OnEnemyDied?.Invoke(this);
            this.gameObject.SetActive(false);
        }
        #endregion

        #region Private Methods
        private void CacheComponents()
        {
            EnemyBehaviourController = GetComponent<EnemyBehaviourController>();
            EnemyHealthController = GetComponent<EnemyHealthController>();
        }

        
        #endregion

    }

}
