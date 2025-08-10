using UnityEngine;

[CreateAssetMenu(fileName = "ExplodePlayerSpell", menuName = "Spells/ExplodePlayerSpell")]
public class ExplodePlayerSpell: Spell
{
    [SerializeField] private ParticleSystem effect;
    [SerializeField] private ParticleSystem indicatorEffect;
    [SerializeField] private float delay;
    [SerializeField] private uint numOfCycles;
    [SerializeField] private float radius;
    public ParticleSystem Effect => effect;
    public ParticleSystem IndicatorEffect => indicatorEffect;
    public uint NumOfCycles => numOfCycles;
    public float Delay => delay;
    public float Radius => radius;
}
