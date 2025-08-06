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

    private IEnumerator SpawnMinions()
    {
        yield return new WaitForSeconds(3);
        for (int i = 0; i < spawnData.MaxConcurrent; i++)
        {
            SpawnMinion();
            yield return new WaitForSeconds(spawnData.Rate);
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
        var followAndAttack = minionInstance.GetOrAddComponent<FollowAndAttackPlayer>();
        followAndAttack.PlayerTransform = playerTransfrom;
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
