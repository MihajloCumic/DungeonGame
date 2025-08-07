using UnityEngine;

public abstract class Spell : ScriptableObject
{
    [SerializeField] private SpellType _spellType;
    // [SerializeField] private GameObject _spellPrefab;
    [SerializeField] private uint _baseDamage;
    [SerializeField] private uint _cooldown;

    public SpellType SpellType => _spellType;
    // public GameObject SpellPrefab => _spellPrefab;
    public uint BaseDamage => _baseDamage;
    public uint Cooldown => _cooldown;
}
