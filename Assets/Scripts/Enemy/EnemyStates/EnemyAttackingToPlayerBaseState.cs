using System.Collections;
using BoardDefenceGame.HardCodeds;
using UnityEngine;

namespace BoardDefenceGame.Enemy.States
{
    public class EnemyAttackingToPlayerBaseState : EnemyAttackingToDefenceItemState
    {
        public EnemyAttackingToPlayerBaseState(EnemyBehaviourController enemyBehaviourController) : base(enemyBehaviourController)
        {
            base.enemyBehaviourController = enemyBehaviourController;
        }

        public override IEnumerator Execute()
        {
            enemyBehaviourController.EnemyAnimator?.SetTrigger(AnimationVariables.EnemyAttackAnimation);
            yield return new WaitForSeconds(1f);
            enemyBehaviourController.Enemy?.OnEnemyReachedPlayerBase();
        }


    }

}
