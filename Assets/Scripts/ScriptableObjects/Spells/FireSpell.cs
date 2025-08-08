using UnityEngine;

[CreateAssetMenu(fileName = "FireSpell", menuName = "Spells/FireSpell")]
public class FireSpell : Spell
{
    [SerializeField] private float _maxDistance;
    [SerializeField] private float _burnRadius;
    public float MaxDistance => _maxDistance;
    public float BurnRadius => _burnRadius;

    public override void DrawIndicator(Vector3 playerPosition, RaycastHit hit, GameObject indicator)
    {
        indicator.SetActive(true);
        var distance = _maxDistance;
        var contrainedYHit = new Vector3(hit.point.x, 0.1f, hit.point.z);
        var direction = (contrainedYHit - playerPosition).normalized;
        var hitPoint = playerPosition + (direction * distance);
        
        var oldScale = indicator.transform.localScale;
        indicator.transform.localScale = new Vector3(oldScale.x, distance, oldScale.z);
        
        indicator.transform.SetPositionAndRotation((playerPosition + hitPoint) / 2f, Quaternion.LookRotation(direction));
        indicator.transform.Rotate(90, 0, 0);
    }
}
