using System.Collections;
using BoardDefenceGame.Core.Grid;
using BoardDefenceGame.DefenceItems;
using BoardDefenceGame.HardCodeds;
using OddieGames.StateMachine;
using UnityEngine;
namespace BoardDefenceGame.Enemy.States
{
    public class EnemyAttackingToDefenceItemState : AbstractState
    {
        protected EnemyBehaviourController enemyBehaviourController;
        DefenceItem attackingItem;
        public EnemyAttackingToDefenceItemState(EnemyBehaviourController enemyBehaviourController)
        {
            this.enemyBehaviourController = enemyBehaviourController;
        }
        public override void Enter()
        {
            this.enemyBehaviourController.ResetDecisionBooleans();
            if (enemyBehaviourController.MovingGrid is DefenseGrid)
            {
                if (((DefenseGrid)enemyBehaviourController.MovingGrid).HasDefenceItem)
                {
                    attackingItem = ((DefenseGrid)enemyBehaviourController.MovingGrid).HoldingDefenceItem;

                }
            }
        }
        public override IEnumerator Execute()
        {
            var enemyAttackCoolDownTime = new WaitForSeconds(this.enemyBehaviourController.Enemy.EnemyFeature.EnemyAttackCoolDown);
            var defenceItemHealthController = attackingItem.DefenceItemHealthController;
            while (!defenceItemHealthController.IsDead)
            {
                this.enemyBehaviourController.EnemyAnimator?.SetTrigger(AnimationVariables.EnemyAttackAnimation);
                defenceItemHealthController.TakeDamage(this.enemyBehaviourController.Enemy.EnemyFeature.EnemyDamage);
                yield return enemyAttackCoolDownTime;
            }
        }


    }
}

