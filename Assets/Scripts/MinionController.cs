using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Pool;

[RequireComponent(typeof(NavMeshAgent))]
public class MinionController : MonoBehaviour
{
    [SerializeField] private MinionStats minionStats;
    private uint _currHealth;
    public IDamagable Player { private get; set; }
    private NavMeshAgent _agent;
    private Animator _animator;
    private Renderer _renderer;
    private Color _originalColor;

    public ObjectPool<MinionController> ObjectPool { get; set; }
    void Awake()
    {
        GetComponent<DamagableMinion>();
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponentInChildren<Animator>();
        _renderer = GetComponentInChildren<Renderer>();
        _originalColor = _renderer.material.color;
        _currHealth = minionStats.MaxHealth;
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
