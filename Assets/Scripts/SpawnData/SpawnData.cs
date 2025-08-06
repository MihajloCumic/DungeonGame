using UnityEngine;

[CreateAssetMenu(fileName = "SpawnData", menuName = "Spawn/Data")]
public class SpawnData : ScriptableObject
{
    [SerializeField] private int poolCapacity;
    [SerializeField] private int maxConcurrent;
    [SerializeField] private float rate;

    public int PoolCapacity => poolCapacity;
    public int MaxConcurrent => maxConcurrent;
    public float Rate => rate;
}
