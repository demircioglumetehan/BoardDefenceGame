using BoardDefenceGame.Enemy;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyFeature", menuName = "Scriptables/EnemyFeature/Create", order = 1)]
public class EnemyFeatureScriptableObject : ScriptableObject    
{
    [field:SerializeField] public int Health { get; private set; }
    [field:SerializeField] public float SpeedBlockPerSecond { get; private set; }
    [field:SerializeField] public BaseEnemy EnemyPrefab { get; private set; }
    [field:SerializeField] public Sprite EnemyIcon { get; private set; }
    [field:SerializeField] public float EnemyDamage { get; private set; }
    [field:SerializeField] public float EnemyAttackCoolDown { get; private set; }
}
