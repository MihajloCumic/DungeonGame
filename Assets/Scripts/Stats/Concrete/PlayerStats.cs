using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Stats/Player")]
public class PlayerStats : BaseStats
{
    [SerializeField] private uint maxMana;
    public uint MaxMana
    {
        get { return maxMana; }
    }

}
