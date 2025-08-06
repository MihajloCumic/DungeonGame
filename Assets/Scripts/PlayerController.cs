using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    public Camera Cemara => _camera;

    [SerializeField] private PlayerStats playerStats;
    private NavMeshAgent _agent;
    public NavMeshAgent Agent => _agent;
    
    public Animator Animator { get; private set; }

    private StateManager _stateManager;

    void Awake()
    {
        Animator = GetComponentInChildren<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = playerStats.MovementSpeed;
        _stateManager = new(this);
    }

    void Start()
    {
        Animator.CrossFade("Idle", 0.1f);   
    }


    void Update()
    {
        _stateManager.Update();
    }

    
}
