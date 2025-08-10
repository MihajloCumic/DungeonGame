using System;
using UnityEngine;

[CreateAssetMenu(fileName = "BossStats", menuName = "Stats/BossStats")]
public class BossStats : BaseStats
{
    [SerializeField] private float startDelay;
    [SerializeField] private float lowerAttackInterval;
    [SerializeField] private float upperAttackInterval;

    public float StartDelay => startDelay;
    public float LowerAttackInterval => lowerAttackInterval;
    public float UpperAttackInterval => upperAttackInterval;
}
