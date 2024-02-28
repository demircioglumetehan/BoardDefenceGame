using System.Collections;
using System.Collections.Generic;
using BoardDefenceGame.Core;
using BoardDefenceGame.Events;
using BoardDefenceGame.ObjectPooler;
using UnityEngine;

namespace BoardDefenceGame.Enemy
{
    public class EnemySpawner : SingletonBase<EnemySpawner>
    {
        #region Fields
        public List<EnemyCounter> KilledEnemies { get; private set; }
        private List<EnemySpawnWaveFeature> enemyWaves;
        private List<BaseEnemy> liveEnemies;
        private bool allEnemiesSpawned = false;
        private bool playerBaseDestroyed = false;
        #endregion

        #region Unity Methods
        private void OnEnable()
        {
            GameEventsUI.OnStartBattleButtonPressed += StartSpawnEnemies;
            GameEvents.OnEnemyDied += UpdateEnemyList;
            GameEvents.OnPlayerBaseDestroyed += OnPlayerBaseDestroyed;

        }

        private void OnDisable()
        {
            GameEventsUI.OnStartBattleButtonPressed -= StartSpawnEnemies;
            GameEvents.OnEnemyDied -= UpdateEnemyList;
            GameEvents.OnPlayerBaseDestroyed -= OnPlayerBaseDestroyed;
        }

        #endregion

        #region Public Methods
        public void Initialize(LevelDataScriptableObject currentLevelFeature)
        {
            enemyWaves = new List<EnemySpawnWaveFeature>(currentLevelFeature.EnemyWaves);
            KilledEnemies = new List<EnemyCounter>();
        }
        #endregion

        #region Private Methods
        private void StartSpawnEnemies()
        {
            StartCoroutine(SpawnEnemiesCor());
        }

        private IEnumerator SpawnEnemiesCor()
        {
            liveEnemies = new List<BaseEnemy>();
            for (int i = 0; i < enemyWaves.Count; i++)
            {
                var enemyWave = enemyWaves[i];
                yield return new WaitForSeconds(enemyWave.TimeToSpawnInitialEnemy);
                var waitTimeBetweenEnemiesForThisWave = new WaitForSeconds(enemyWave.TimeBetweenSpawningEnemies);
                for (int j = 0; j < enemyWave.SpawningEnemyAmount; j++)
                {
                    var spawningGrid = BoardManager.Instance.GetRandomEnemySpawnerGrid();
                    if (!spawningGrid)
                        Debug.LogError("There is not available spawning Grid!");
                    var spawnedEnemy = ObjectPoolManager.Instance.GetEnemyPool(enemyWave.spawningEnemyFeature).GetPooledObject(spawningGrid.transform.position).GetComponent<BaseEnemy>();
                    spawnedEnemy.Initialize(enemyWave.spawningEnemyFeature, spawningGrid);
                    liveEnemies.Add(spawnedEnemy);
                    yield return waitTimeBetweenEnemiesForThisWave;
                }
            }
            allEnemiesSpawned = true;
        }

        private void UpdateEnemyList(BaseEnemy deadEnemy)
        {
            UpdateKilledEnemyList(deadEnemy);
            UpdateLiveEnemyList(deadEnemy);

        }

        private void UpdateLiveEnemyList(BaseEnemy deadEnemy)
        {
            if (liveEnemies.Contains(deadEnemy))
            {
                liveEnemies.Remove(deadEnemy);
                if (allEnemiesSpawned && liveEnemies.Count == 0)
                {
                    if (playerBaseDestroyed)
                        return;
                    GameEvents.OnAllEnemiesDied?.Invoke();
                }
            }
        }

        private void UpdateKilledEnemyList(BaseEnemy deadEnemy)
        {
            var killedEnemyClass = KilledEnemies.Find(i => i.EnemyFeature == deadEnemy.EnemyFeature);

            if (killedEnemyClass == null)
            {
                killedEnemyClass = new EnemyCounter(0, deadEnemy.EnemyFeature);
                KilledEnemies.Add(killedEnemyClass);
            }
            killedEnemyClass.EnemyAmount++;
        }
        private void OnPlayerBaseDestroyed()
        {
            playerBaseDestroyed = true;
        }
        #endregion

    }
}

[System.Serializable]
public class EnemyCounter
{
    public int EnemyAmount;
    public EnemyFeatureScriptableObject EnemyFeature;

    public EnemyCounter(int enemyAmount, EnemyFeatureScriptableObject enemyFeature)
    {
        this.EnemyAmount = enemyAmount;
        this.EnemyFeature = enemyFeature;
    }
}