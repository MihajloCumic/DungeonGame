using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Pool;

[RequireComponent(typeof(NavMeshAgent))]
public class MinionController : MonoBehaviour
{
    [SerializeField] private MinionStats minionStats;
    public IDamagable Player { private get; set; }
    private NavMeshAgent _agent;
    private Animator _animator;

    public ObjectPool<MinionController> ObjectPool { get; set; }
    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        _animator.CrossFade("Idle", 0.1f);
    }

    public void ReleaseMe()
    {
        ObjectPool.Release(this);
    }
    
}
