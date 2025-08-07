using UnityEngine;

[CreateAssetMenu(fileName = "FireSpell", menuName = "Spells/FireSpell")]
public class FireSpell : Spell
{
    [SerializeField] float _maxDistance;
    [SerializeField] float _burnRadius;

    public float MaxDistance => _maxDistance;
    public float BurnRadius => _burnRadius;
}
