using System;
using BoardDefenceGame.Enemy;

namespace BoardDefenceGame.Events
{
    public static class GameEvents
    {
        public static Action<BaseEnemy> OnEnemyDied;
        public static Action OnAllEnemiesDied;
        public static Action OnPlayerBaseDestroyed;
        public static Action<LevelDataScriptableObject> OnCurrentLevelSet;
        public static Action<BaseEnemy> OnEnemyAttackedToPlayerBase;

    }
}
