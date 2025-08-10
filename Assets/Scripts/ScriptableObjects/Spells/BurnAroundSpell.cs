using UnityEngine;

[CreateAssetMenu(fileName = "BurnAroundSpell", menuName = "Spells/BurnAroundSpell")]
public class BurnAroundSpell: Spell
{
    [SerializeField] private ParticleSystem effect;
    [SerializeField] private float delay;
    [SerializeField] private float duration;
    public ParticleSystem Effect => effect;
    public float Delay => delay;
    public float Duration => duration;
}
