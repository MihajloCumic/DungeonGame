using UnityEngine;

[CreateAssetMenu(fileName = "ExplodePlayerSpell", menuName = "Spells/ExplodePlayerSpell")]
public class ExplodePlayerSpell: Spell
{
    [SerializeField] private ParticleSystem effect;
    [SerializeField] private float delay;
    public ParticleSystem Effect => effect;
    public float Delay => delay;
}
