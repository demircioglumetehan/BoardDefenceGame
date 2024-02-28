using System;
using System.Collections;
using System.Collections.Generic;
using BoardDefenceGame.DefenceItems;
using UnityEngine;

namespace BoardDefenceGame.ObjectPooler
{
    public class ObjectPoolManager : SingletonBase<ObjectPoolManager>
    {
        #region Fields
        private List<DefenceItemPoolIdentifier> defenceItemPools = new List<DefenceItemPoolIdentifier>();
        private List<BulletPoolIdentifier> bulletPools = new List<BulletPoolIdentifier>();
        private List<EnemyPoolIdentifier> enemyPools = new List<EnemyPoolIdentifier>();
        #endregion

        #region Public Methods
        public void Initialize(LevelDataScriptableObject currentLevel)
        {
            for (int i = 0; i < currentLevel.DefenceItems.Count; i++)
            {
                var defenceItemFeature = currentLevel.DefenceItems[i].SpawnableDefenceItemFeature;
                CreateDefenceItemObjectPooler(currentLevel.DefenceItems[i].TotalSpawnableDefenceItemAmount, defenceItemFeature);
                CreateBulletObjectPooler(defenceItemFeature);
            }
            var totalSpawningEnemies = CalculateTotalEnemyCounts(currentLevel);
            for (int i = 0; i < totalSpawningEnemies.Count; i++)
            {
                var enemyFeature = totalSpawningEnemies[i].EnemyFeature;
                CreateEnemyObjectPooler(totalSpawningEnemies[i].EnemyAmount, enemyFeature);
            }
        }

        public ObjectPooler GetDefenceItemPool(DefenceItemFeatureScriptableObject draggingDefenceItemFeature)
        {
            var defenceItemPoolIdentifier = defenceItemPools.Find(i => i.ItemFeature == draggingDefenceItemFeature);
            return defenceItemPoolIdentifier.ItemPool;
        }

        public ObjectPooler GetBulletPool(DefenceItemFeatureScriptableObject draggingDefenceItemFeature)
        {
            var bulletPoolIdentifier = bulletPools.Find(i => i.ItemFeature == draggingDefenceItemFeature);
            return bulletPoolIdentifier.ItemPool;
        }

        public ObjectPooler GetEnemyPool(EnemyFeatureScriptableObject enemyFeature)
        {
            var bulletPoolIdentifier = enemyPools.Find(i => i.EnemyFeature == enemyFeature);
            return bulletPoolIdentifier.EnemyPool;
        }
        #endregion

        #region Priate Methods
        private List<EnemyCounter> CalculateTotalEnemyCounts(LevelDataScriptableObject currentLevel)
        {
            List<EnemyCounter> totalSpawningEnemies = new List<EnemyCounter>();
            foreach (var enemyWave in currentLevel.EnemyWaves)
            {
                EnemyCounter addingEnemy = totalSpawningEnemies.Find(i => i.EnemyFeature == enemyWave.spawningEnemyFeature);
                if (addingEnemy == null)
                {
                    addingEnemy = new EnemyCounter(0, enemyWave.spawningEnemyFeature);
                    totalSpawningEnemies.Add(addingEnemy);
                }
                addingEnemy.EnemyAmount += enemyWave.SpawningEnemyAmount;
            }
            return totalSpawningEnemies;
        }

        private void CreateBulletObjectPooler(DefenceItemFeatureScriptableObject defenceItemFeature, int spawnAmount = 10)
        {
            var poolGameObject = new GameObject(defenceItemFeature.name + " Bullet Pool");
            poolGameObject.transform.SetParent(transform);
            var bulletObjectPool = poolGameObject.AddComponent<ObjectPooler>();
            bulletObjectPool.SetPoolingObject(defenceItemFeature.DefenceItemBullet.gameObject);
            bulletObjectPool.GeneratePoolObjects(spawnAmount);
            bulletPools.Add(new BulletPoolIdentifier(bulletObjectPool, defenceItemFeature));
        }

        private void CreateDefenceItemObjectPooler(int spawnAmount, DefenceItemFeatureScriptableObject defenceItemFeature)
        {
            var poolGameObject = new GameObject(defenceItemFeature.name + "Pool");
            poolGameObject.transform.SetParent(transform);
            var defenceItemObjectPool = poolGameObject.AddComponent<ObjectPooler>();
            defenceItemObjectPool.SetPoolingObject(defenceItemFeature.DefenceItemPrefab.gameObject);
            defenceItemObjectPool.GeneratePoolObjects(spawnAmount);
            defenceItemPools.Add(new DefenceItemPoolIdentifier(defenceItemObjectPool, defenceItemFeature));
        }

        private void CreateEnemyObjectPooler(int enemyAmount, EnemyFeatureScriptableObject enemyFeature)
        {
            var poolGameObject = new GameObject(enemyFeature.name + "Pool");
            poolGameObject.transform.SetParent(transform);
            var enemyObjectPool = poolGameObject.AddComponent<ObjectPooler>();
            enemyObjectPool.SetPoolingObject(enemyFeature.EnemyPrefab.gameObject);
            enemyObjectPool.GeneratePoolObjects(enemyAmount);
            enemyPools.Add(new EnemyPoolIdentifier(enemyObjectPool, enemyFeature));
        }
        #endregion

    }
    [System.Serializable]
    public class DefenceItemPoolIdentifier
    {
        public ObjectPooler ItemPool { get; private set; }
        public DefenceItemFeatureScriptableObject ItemFeature { get; private set; }

        public DefenceItemPoolIdentifier(ObjectPooler itemPool, DefenceItemFeatureScriptableObject itemFeature)
        {
            ItemPool = itemPool;
            ItemFeature = itemFeature;
        }
    }
    [System.Serializable]
    public class BulletPoolIdentifier
    {
        public ObjectPooler ItemPool { get; private set; }
        public DefenceItemFeatureScriptableObject ItemFeature { get; private set; }

        public BulletPoolIdentifier(ObjectPooler itemPool, DefenceItemFeatureScriptableObject itemFeature)
        {
            ItemPool = itemPool;
            ItemFeature = itemFeature;
        }
    }
    [System.Serializable]
    public class EnemyPoolIdentifier
    {
        public ObjectPooler EnemyPool { get; private set; }
        public EnemyFeatureScriptableObject EnemyFeature { get; private set; }

        public EnemyPoolIdentifier(ObjectPooler itemPool, EnemyFeatureScriptableObject itemFeature)
        {
            EnemyPool = itemPool;
            EnemyFeature = itemFeature;
        }
    }



}
