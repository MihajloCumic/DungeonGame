using UnityEngine;

[CreateAssetMenu(fileName = "BurnAroundSpell", menuName = "Spells/BurnAroundSpell")]
public class BurnAroundSpell: Spell
{
    [SerializeField] private ParticleSystem indicatorEffect;
    [SerializeField] private ParticleSystem hitEffect;
    [SerializeField] private float radius;
    [SerializeField] private float delay;
    [SerializeField] private float duration;
    public ParticleSystem IndicatorEffect => indicatorEffect;
    public ParticleSystem HitEffect => hitEffect;
    public float Radius => radius;
    public float Delay => delay;
    public float Duration => duration;
}
