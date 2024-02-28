using System;
using System.Collections.Generic;
using System.Linq;
using BoardDefenceGame.Enemy;
using BoardDefenceGame.Events;
using BoardDefenceGame.ObjectPooler;
using UnityEngine;

namespace BoardDefenceGame.DefenceItems
{
    public class DefenceItemAttackController : MonoBehaviour
    {
        public Action<float> OnDefenceItemAttacked;

        #region Fields
        private BoxCollider2D attackCollider;
        private DefenceItemFeatureScriptableObject itemFeature;
        private List<BaseEnemy> insideRangeEnemies = new List<BaseEnemy>();
        private bool canAttack = false;
        #endregion

        #region Unity Methods
        private void Awake()
        {
            attackCollider = GetComponent<BoxCollider2D>();
        }

        private void OnEnable()
        {
            GameEvents.OnEnemyDied += CheckIfEnemyInsideRange;
        }

        private void OnDisable()
        {
            GameEvents.OnEnemyDied -= CheckIfEnemyInsideRange;
        }

        #endregion

        #region OnTriggerXX Methods
        private void OnTriggerEnter2D(Collider2D collision)
        {

            if (collision.TryGetComponent<BaseEnemy>(out var baseEnemy))
            {
                insideRangeEnemies.Add(baseEnemy);

                if (canAttack)
                    CheckForAttacking();
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent<BaseEnemy>(out var baseEnemy))
            {
                insideRangeEnemies.Remove(baseEnemy);
            }
        }
        #endregion

        #region Public Methods
        public void Initialize(DefenceItemFeatureScriptableObject itemFeature)
        {
            this.itemFeature = itemFeature;
            switch (itemFeature.AttackType)
            {
                case AttackType.None:
                    break;
                case AttackType.Forward:
                    SetAttackRangeForward(itemFeature.RangeInBlocks);
                    break;
                case AttackType.AllDirections:
                    SetAttackRangeAllDirections(itemFeature.RangeInBlocks);

                    break;
                default:
                    break;
            }
            canAttack = true;
            CheckForAttacking();
        }

        public void SetAttackRangeForward(int rangeInBlocks)
        {
            attackCollider.offset = new Vector2(0f, (rangeInBlocks / 2f));
            attackCollider.size = new Vector2(.9f, (rangeInBlocks + 1) * .9f);
        }

        public void SetAttackRangeAllDirections(int rangeInBlocks)
        {
            attackCollider.offset = Vector2.zero;
            attackCollider.size = (1 + (2 * rangeInBlocks)) * .9f * Vector2.one;
        }

        public void EnableAttackingStatus()
        {
            canAttack = true;
            CheckForAttacking();
        }

        public void DisableAttackingStatus()
        {
            canAttack = false;
        }
        #endregion

        #region Private Methods
        private void CheckIfEnemyInsideRange(BaseEnemy enemy)
        {
            if (insideRangeEnemies.Contains(enemy))
            {
                insideRangeEnemies.Remove(enemy);
            }
        }
        private void ShootBullet(BaseEnemy attackingEnemy)
        {
            var bullet = ObjectPoolManager.Instance.GetBulletPool(itemFeature).GetPooledObject(transform.position).GetComponent<Bullet>();
            bullet.ShootBullet(attackingEnemy, itemFeature.Damage);
            OnDefenceItemAttacked?.Invoke(itemFeature.AttackingTimeInterval);
        }
        private void CheckForAttacking()
        {
            if (insideRangeEnemies.Count > 0)
            {
                var attackingEnemy = insideRangeEnemies.First();
                ShootBullet(attackingEnemy);
            }
        }
        #endregion

    }

}
