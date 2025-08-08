using UnityEngine;

public abstract class Spell : ScriptableObject
{
    [SerializeField] private SpellType _spellType;
    [SerializeField] private uint _baseDamage;
    [SerializeField] private uint _cooldown;
    [SerializeField] protected GameObject _indicator;

    public SpellType SpellType => _spellType;
    public uint BaseDamage => _baseDamage;
    public uint Cooldown => _cooldown;
    public GameObject Indicator => _indicator;

    public virtual void DrawIndicator(Vector3 playerPosition, RaycastHit hit, GameObject indicator)
    {
        return;
    }
}
