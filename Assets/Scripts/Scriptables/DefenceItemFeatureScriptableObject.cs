using UnityEngine;
using BoardDefenceGame.DefenceItems;

[CreateAssetMenu(fileName = "DefenceItemFeature", menuName = "Scriptables/DefenceItemFeature/Create", order = 1)]
public class DefenceItemFeatureScriptableObject : ScriptableObject
{
    [field: SerializeField] public int Damage { get; private set; }
    [field: SerializeField] public int RangeInBlocks { get; private set; }
    [field: SerializeField] public int AttackingTimeInterval { get; private set; }
    [field: SerializeField] public int Health { get; private set; }
    [field: SerializeField] public AttackType AttackType { get; private set; }
    [field: SerializeField] public string ItemName { get; private set; }
    [field: SerializeField] public DefenceItem DefenceItemPrefab { get; private set; }
    [field: SerializeField] public Sprite DefenceItemSprite { get; private set; }
    [field: SerializeField] public Bullet DefenceItemBullet { get; private set; }
}

public enum AttackType
{
    None = 0,
    Forward = 1,
    AllDirections = 2
}

