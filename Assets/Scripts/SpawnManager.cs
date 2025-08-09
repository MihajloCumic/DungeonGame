using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private MinionController minionPrefab;
    [SerializeField] private SpawnData spawnData;
    [SerializeField] private Transform playerTransfrom;

    private ObjectPool<MinionController> _objectPool;


    void Awake()
    {
        _objectPool = new ObjectPool<MinionController>(
            CreateMinion,
            OnGetFromPool,
            OnReleaseToPool,
            OnDestroyPooledObject,
            true,
            spawnData.MaxConcurrent,
            spawnData.PoolCapacity
        );
    }

    void Start()
    {
        StartCoroutine(SpawnMinions());
    }

    

    private IEnumerator InitialWait()
    {
        yield return new WaitForSeconds(spawnData.InitialWait);
    }

    private IEnumerator CycleWait()
    {
        var cycleWait = Random.Range(spawnData.SpawnCycleIntervalLower, spawnData.SpawnCycleIntervalUpper + 1);
        yield return new WaitForSeconds(cycleWait);
    }

    private IEnumerator SpawnMinions()
    {
        yield return InitialWait();
        while (true)
        {
            for (int i = 0; i < spawnData.MaxConcurrent; i++)
            {
                SpawnMinion();
                yield return new WaitForSeconds(spawnData.Rate);
            }   
            yield return CycleWait();
        }
    }

    private void SpawnMinion()
    {
        var minion = _objectPool.Get();
        minion.transform.SetPositionAndRotation(transform.position, Quaternion.identity);
    }

    private MinionController CreateMinion()
    {
        MinionController minionInstance = Instantiate(minionPrefab);
        minionInstance.ObjectPool = _objectPool;
        return minionInstance;
    }

    private void OnGetFromPool(MinionController pooledMinion)
    {
        pooledMinion.gameObject.SetActive(true);
    }

    private void OnReleaseToPool(MinionController releasedMinion)
    {
        releasedMinion.gameObject.SetActive(false);
    }

    private void OnDestroyPooledObject(MinionController toDestroyMinion)
    {
        Destroy(toDestroyMinion);
    }
}
