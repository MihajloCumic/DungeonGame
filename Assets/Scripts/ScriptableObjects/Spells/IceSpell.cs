using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "IceSpell", menuName = "Spells/IceSpell")]
public class IceSpell : Spell
{
    [SerializeField] private float maxDistance;
    [SerializeField] private uint numOfCycles;
    [SerializeField] private float waitBetweenCycles;
    [SerializeField] private float slowDownDuration;
    [SerializeField] private float spellRadius;
    [SerializeField] private ParticleSystem hitEffect;
    [SerializeField] private ParticleSystem fallingEffect;
    public float MaxDistance => maxDistance;
    public uint NumOfCycles => numOfCycles;
    public float WaitBetweenCycles => waitBetweenCycles;
    public float SlowDownDuration => slowDownDuration;
    public float SpellRadius => spellRadius;
    public ParticleSystem HitEffect => hitEffect;
    public ParticleSystem FallingEffect => fallingEffect;

    public override void DrawIndicator(Vector3 playerPosition, RaycastHit hit, GameObject indicator)
    {
        var distance = Vector3.Distance(playerPosition, hit.point);
        distance = distance > maxDistance ? maxDistance : distance;
        var direction = (hit.point - playerPosition).normalized;
        var center = playerPosition + (direction * distance);
        if (!indicator.activeInHierarchy)
        {
            var scale = indicator.transform.localScale;
            var diameter = SpellRadius * 2f;
            indicator.transform.localScale = new Vector3(diameter, diameter, scale.z);
            indicator.SetActive(true);
        }
        indicator.transform.position = new Vector3(center.x, 0.2f, center.z);
    }
}
