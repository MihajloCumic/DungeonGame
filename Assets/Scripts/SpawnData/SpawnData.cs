using UnityEngine;

[CreateAssetMenu(fileName = "SpawnData", menuName = "Spawn/Data")]
public class SpawnData : ScriptableObject
{
    [SerializeField] private float initialWait;
    [SerializeField] private int poolCapacity;
    [SerializeField] private int maxConcurrent;
    [SerializeField] private float rate;
    [SerializeField] private int spawnCycleIntervalLower;
    [SerializeField] private int spawnCycleIntervalUpper;


    public float InitialWait => initialWait;
    public int PoolCapacity => poolCapacity;
    public int MaxConcurrent => maxConcurrent;
    public float Rate => rate;
    public float SpawnCycleIntervalLower => spawnCycleIntervalLower;
    public float SpawnCycleIntervalUpper => spawnCycleIntervalUpper;
}
