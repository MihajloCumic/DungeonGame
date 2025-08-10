using UnityEngine;

[CreateAssetMenu(fileName = "FireSpell", menuName = "Spells/FireSpell")]
public class FireSpell : Spell
{
    [SerializeField] private float maxDistance;
    [SerializeField] private GameObject hitEffect;
    [SerializeField] private GameObject pathEffect;
    public float MaxDistance => maxDistance;
    public GameObject HitEffect => hitEffect;
    public GameObject PathEffect => pathEffect;

    public override void DrawIndicator(Vector3 playerPosition, RaycastHit hit, GameObject indicator)
    {
        indicator.SetActive(true);
        var distance = maxDistance;
        var contrainedYHit = new Vector3(hit.point.x, 0.1f, hit.point.z);
        var direction = (contrainedYHit - playerPosition).normalized;
        var hitPoint = playerPosition + (direction * distance);
        
        var oldScale = indicator.transform.localScale;
        indicator.transform.localScale = new Vector3(oldScale.x, distance, oldScale.z);
        
        indicator.transform.SetPositionAndRotation((playerPosition + hitPoint) / 2f, Quaternion.LookRotation(direction));
        indicator.transform.Rotate(90, 0, 0);
    }
}
