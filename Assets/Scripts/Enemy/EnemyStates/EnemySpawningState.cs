using System.Collections;
using OddieGames.StateMachine;
using UnityEngine;
using DG.Tweening;

namespace BoardDefenceGame.Enemy.States
{
    public class EnemySpawningState : AbstractState
    {
        EnemyBehaviourController enemyBehaviourController;

        public EnemySpawningState(EnemyBehaviourController enemyBehaviourController)
        {
            this.enemyBehaviourController = enemyBehaviourController;
        }

        public override IEnumerator Execute()
        {
            enemyBehaviourController.transform.localScale = Vector3.zero;
            enemyBehaviourController.transform.DOScale(Vector3.one, .5f);
            yield return new WaitForSeconds(.5f);

        }

    }

}
