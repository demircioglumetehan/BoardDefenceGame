using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Scriptables/LevelData/Create", order = 1)]
public class LevelDataScriptableObject : ScriptableObject
{
    [field:SerializeField] public Vector2Int BoardDimensions { get; private set; }
    [field:SerializeField] public int LevelNumber { get; private set; }
    [field: SerializeField,Range(1,5)] public int PlayerBaseHealth { get; private set; }
    [field:SerializeField] public List<EnemySpawnWaveFeature> EnemyWaves { get; private set; }
    [field:SerializeField] public List<LevelBasedDefenceItem> DefenceItems { get; private set; }
}
[System.Serializable]
public class EnemySpawnWaveFeature
{
    [field: SerializeField,Range(0f, 1f)] public float TimeToSpawnInitialEnemy;
    [field: SerializeField,Range(0f, 1f)] public float TimeBetweenSpawningEnemies;
    [field: SerializeField] public int SpawningEnemyAmount { get; private set; }
    [field: SerializeField] public EnemyFeatureScriptableObject spawningEnemyFeature { get; private set; }
}
[System.Serializable]
public class LevelBasedDefenceItem
{
    [field: SerializeField] public int TotalSpawnableDefenceItemAmount { get; private set; }
    [field: SerializeField] public DefenceItemFeatureScriptableObject SpawnableDefenceItemFeature { get; private set; }
    public LevelBasedDefenceItem(int totalSpawnableDefenceItemAmount, DefenceItemFeatureScriptableObject spawnableDefenceItemFeature)
    {
        TotalSpawnableDefenceItemAmount = totalSpawnableDefenceItemAmount;
        SpawnableDefenceItemFeature = spawnableDefenceItemFeature;
    }

    internal void DecrementTotalSpawnableDefenceItemAmount(int decrementAmount)
    {
        TotalSpawnableDefenceItemAmount -= decrementAmount;
    }
}