using UnityEngine;

public abstract class Attack : ScriptableObject
{
    [SerializeField] private uint _baseDamage;
    [SerializeField] private float _cooldown;
    [SerializeField] private float _maxDistance;

    public uint BaseDamage => _baseDamage;
    public float Cooldown => _cooldown;
    public float MaxDistance => _maxDistance;
    
}
