using System.Collections;
using OddieGames.StateMachine;

namespace BoardDefenceGame.Enemy.States
{
    public class EnemyDeathState : AbstractState
    {
        EnemyBehaviourController enemyBehaviourController;

        public EnemyDeathState(EnemyBehaviourController enemyBehaviourController)
        {
            this.enemyBehaviourController = enemyBehaviourController;
        }

        public override IEnumerator Execute()
        {
            yield return null;

        }


    }

}
