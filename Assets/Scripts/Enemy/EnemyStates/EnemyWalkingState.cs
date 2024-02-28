using System.Collections;
using BoardDefenceGame.HardCodeds;
using OddieGames.StateMachine;
using UnityEngine;

namespace BoardDefenceGame.Enemy.States
{
    public class EnemyWalkingState : AbstractState
    {
        EnemyBehaviourController enemyBehaviourController;

        public EnemyWalkingState(EnemyBehaviourController enemyBehaviourController)
        {
            this.enemyBehaviourController = enemyBehaviourController;
        }

        public override void Enter()
        {
            this.enemyBehaviourController.ResetDecisionBooleans();
        }

        public override IEnumerator Execute()
        {
            this.enemyBehaviourController.EnemyAnimator?.SetTrigger(AnimationVariables.EnemyWalkAnimation);
            var speed = enemyBehaviourController.Enemy.EnemyFeature.SpeedBlockPerSecond;
            while (Vector3.Distance(enemyBehaviourController.transform.position, enemyBehaviourController.MovingGrid.transform.position) > .1f)
            {
                enemyBehaviourController.transform.position += Time.deltaTime * speed * Vector3.down;
                yield return null;
            }
            enemyBehaviourController.DecideNextStateAfterWalk();
        }

    }

}
