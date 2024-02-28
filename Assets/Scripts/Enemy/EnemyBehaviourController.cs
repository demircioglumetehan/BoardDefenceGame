using BoardDefenceGame.Core;
using BoardDefenceGame.Core.Grid;
using BoardDefenceGame.Enemy.States;
using OddieGames.StateMachine;
using UnityEngine;

namespace BoardDefenceGame.Enemy
{
    public class EnemyBehaviourController : MonoBehaviour
    {
        #region Fields
        [field: SerializeField] public Animator EnemyAnimator { get; private set; }
        public BaseEnemy Enemy { get; private set; }
        public GridBase MovingGrid { get; private set; }

        private StateMachine stateMachine;
        private AbstractState EnemyWalkingState;
        private AbstractState EnemyAttackingToDefenceItemState;
        private AbstractState EnemyDeathState;
        private AbstractState EnemySpawningState;
        private AbstractState EnemyAttackingToPlayerBaseState;

        private bool enemyDied = false;
        private bool shouldContinueWalk = false;
        private bool shouldAttack = false;
        private bool reachedPlayerBase = false;
        #endregion

        #region Public Methods
        public void Initialize(BaseEnemy baseEnemy, GridBase spawningGrid)
        {
            enemyDied = false;
            MovingGrid = BoardManager.Instance.GetBelowGrid(spawningGrid);
            this.Enemy = baseEnemy;
            this.Enemy.OnEnemyDied += OnEnemyDied;
            stateMachine = new StateMachine();
            this.EnemyWalkingState = new EnemyWalkingState(this);
            this.EnemyAttackingToDefenceItemState = new EnemyAttackingToDefenceItemState(this);
            this.EnemyDeathState = new EnemyDeathState(this);
            this.EnemySpawningState = new EnemySpawningState(this);
            this.EnemyAttackingToPlayerBaseState = new EnemyAttackingToPlayerBaseState(this);

            EnemySpawningState.AddLink(new Link(EnemyWalkingState));
            EnemyWalkingState.AddLink(new ConditionalLink(EnemyWalkingState, () => shouldContinueWalk));
            EnemyWalkingState.AddLink(new ConditionalLink(EnemyAttackingToDefenceItemState, () => shouldAttack));
            EnemyWalkingState.AddLink(new ConditionalLink(EnemyAttackingToPlayerBaseState, () => reachedPlayerBase));
            EnemyAttackingToDefenceItemState.AddLink(new Link(EnemyWalkingState));

            stateMachine.AddAnyStateLink(new ConditionalLink(EnemyDeathState, () => enemyDied));

            stateMachine.Run(EnemySpawningState);

        }

        public void ResetDecisionBooleans()
        {
            shouldContinueWalk = false;
            shouldAttack = false;


        }

        public void DecideNextStateAfterWalk()
        {
            if (MovingGridHasDefenceItem())
            {
                shouldAttack = true;
                return;
            }

            MovingGrid = BoardManager.Instance.GetBelowGrid(MovingGrid);
            if (!MovingGrid)
            {
                reachedPlayerBase = true;
                return;
            }
            if (MovingGridHasDefenceItem())
            {
                shouldAttack = true;
                return;
            }
            shouldContinueWalk = true;
        }
        #endregion

        #region Private Methods
        private void OnEnemyDied()
        {
            enemyDied = true;
            this.Enemy.OnEnemyDied -= OnEnemyDied;
        }

        private bool MovingGridHasDefenceItem()
        {
            if (MovingGrid is DefenseGrid)
            {
                if (((DefenseGrid)MovingGrid).HasDefenceItem)
                {
                    return true;
                }
            }
            return false;
        }

        private void OnDisable()
        {

            stateMachine?.Stop();
        }
        #endregion

    }
}