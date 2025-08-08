using UnityEngine;

[CreateAssetMenu(fileName = "SpellSet", menuName = "Scriptable Objects/SpellSet")]
public class SpellSet : ScriptableObject
{
    [SerializeField] private Spell firstSpell;
    public Spell FirstSpell => firstSpell;
}
