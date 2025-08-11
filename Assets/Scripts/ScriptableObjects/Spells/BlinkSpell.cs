using UnityEngine;

[CreateAssetMenu(fileName = "BlinkSpell", menuName = "Spells/BlinkSpell")]
public class BlinkSpell: Spell
{
    [SerializeField] private ParticleSystem effect;
    [SerializeField] private float delay;
    [SerializeField] private float distance;
    public ParticleSystem Effect => effect;
    public float Delay => delay;
    public float Distance => distance;
}
