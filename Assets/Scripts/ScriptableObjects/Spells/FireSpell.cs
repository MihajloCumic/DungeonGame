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
    
}
