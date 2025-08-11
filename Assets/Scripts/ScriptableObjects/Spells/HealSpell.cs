using UnityEngine;

[CreateAssetMenu(fileName = "HealSpell", menuName = "Spells/HealSpell")]
public class HealSpell: Spell
{
    [SerializeField] private uint healAmount;
    [SerializeField] private ParticleSystem effect;
    [SerializeField] private float delay;
    public uint HealAmount => healAmount;
    public ParticleSystem Effect => effect;
    public float Delay => delay;
    
}
