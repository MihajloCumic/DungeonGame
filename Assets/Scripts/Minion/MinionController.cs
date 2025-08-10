using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Pool;

[RequireComponent(typeof(NavMeshAgent), typeof(AnimationManager))]
public class MinionController : MonoBehaviour
{
    [SerializeField] private MinionStats minionStats;
    [SerializeField] private MinionAttack minionAttack;
    public IDamagable Player { get; private set; }
    private NavMeshAgent _agent;
    private AnimationManager _animationManager;
    private FollowAndAttackState _followAndAttackState;

    public ObjectPool<MinionController> ObjectPool { get; set; }
    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = minionStats.MovementSpeed;
        _animationManager = GetComponent<AnimationManager>();
        
    }

    public void SetPlayerAndState(IDamagable player)
    {
        Player = player;
        _followAndAttackState = new(
            minionAttack,
            transform,
            Player,
            _agent,
            _animationManager
        );   
    }

    void Update()
    {
        _followAndAttackState.UpdateState();
    }

    public void ReleaseMe()
    {
        ObjectPool.Release(this);
    }
    
}
