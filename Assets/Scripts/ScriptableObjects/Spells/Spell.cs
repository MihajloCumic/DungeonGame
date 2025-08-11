using UnityEngine;

public abstract class Spell : ScriptableObject
{
    [SerializeField] private uint _baseDamage;
    [SerializeField] private uint _cooldown;
    [SerializeField] protected GameObject _indicator;
    [SerializeField] protected bool isBlocking;

    public uint BaseDamage => _baseDamage;
    public uint Cooldown => _cooldown;
    public GameObject Indicator => _indicator;
    public bool IsBlocking => isBlocking;

    public virtual void DrawIndicator(Vector3 playerPosition, RaycastHit hit, GameObject indicator)
    {
        return;
    }
}
