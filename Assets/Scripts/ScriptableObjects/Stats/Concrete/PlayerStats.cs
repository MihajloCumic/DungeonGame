using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Stats/Player")]
public class PlayerStats : BaseStats
{
    [SerializeField] private uint maxMana;
    [SerializeField] private Attack meleAttack;

    public uint MaxMana => maxMana;
    public Attack MeleAttack => meleAttack;

}
