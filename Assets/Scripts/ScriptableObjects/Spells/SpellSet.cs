using UnityEngine;

[CreateAssetMenu(fileName = "SpellSet", menuName = "Scriptable Objects/SpellSet")]
public class SpellSet : ScriptableObject
{
    [SerializeField] private Spell firstSpell;
    [SerializeField] private Spell secondSpell;
    public Spell FirstSpell => firstSpell;
    public Spell SecondSpell => secondSpell;
}
