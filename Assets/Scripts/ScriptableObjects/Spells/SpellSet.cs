using UnityEngine;

[CreateAssetMenu(fileName = "SpellSet", menuName = "Scriptable Objects/SpellSet")]
public class SpellSet : ScriptableObject
{
    [SerializeField] private Spell firstSpell;
    [SerializeField] private Spell secondSpell;
    [SerializeField] private Spell thirdSpell;
    public Spell FirstSpell => firstSpell;
    public Spell SecondSpell => secondSpell;
    public Spell ThirdSpell => thirdSpell;
}
