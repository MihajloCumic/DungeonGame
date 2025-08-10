using UnityEngine;

[CreateAssetMenu(fileName = "IceSpell", menuName = "Spells/IceSpell")]
public class IceSpell : Spell
{
    [SerializeField] private float maxDistance;
    [SerializeField] private uint numOfCycles;
    [SerializeField] private float waitBetweenCycles;
    [SerializeField] private float slowDownDuration;
    [SerializeField] private float spellRadius;
    [SerializeField] private GameObject hitEffect;
    [SerializeField] private GameObject fallingEffect;
    public float MaxDistance => maxDistance;
    public uint NumOfCycles => numOfCycles;
    public float WaitBetweenCycles => waitBetweenCycles;
    public float SlowDownDuration => slowDownDuration;
    public float SpellRadius => spellRadius;
    public GameObject HitEffect => hitEffect;
    public GameObject FallingEffect => fallingEffect;

    public override void DrawIndicator(Vector3 playerPosition, RaycastHit hit, GameObject indicator)
    {
        
    }
}
