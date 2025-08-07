using UnityEngine;

[CreateAssetMenu(fileName = "FireSpell", menuName = "Spells/FireSpell")]
public class FireSpell : Spell
{
    [SerializeField] private float _maxDistance;
    [SerializeField] private float _burnRadius;
    [SerializeField] private GameObject _indicator;

    public float MaxDistance => _maxDistance;
    public float BurnRadius => _burnRadius;
    public GameObject Indicator => _indicator;


    public void DrawIndicator(Vector3 playerPosition, Ray mouseRay, GameObject indicator)
    {
        if (Physics.Raycast(mouseRay, out RaycastHit hit))
        {
            var distance = Vector3.Distance(playerPosition, hit.point);
            var oldScale = indicator.transform.localScale;
            indicator.transform.localScale = new Vector3(oldScale.x, distance, oldScale.z);
            indicator.transform.position = (playerPosition + hit.point) / 2f;

            Vector3 direction = hit.point - playerPosition;
            indicator.transform.rotation = Quaternion.LookRotation(direction);
            indicator.transform.Rotate(90, 0, 0);
        }
        Debug.Log(indicator.activeInHierarchy);
    }
    
}
