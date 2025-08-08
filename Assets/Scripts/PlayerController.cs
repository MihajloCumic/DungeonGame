using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(AnimationManager))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private SpellSet spellSet;
    public SpellSet SpellSet => spellSet;
    [SerializeField] private Camera _camera;
    public Camera Cemara => _camera;
    [SerializeField] private PlayerStats playerStats;
    public PlayerStats PlayerStats => playerStats; 
    private NavMeshAgent _agent;
    public NavMeshAgent Agent => _agent;
    private AnimationManager _animationManager;
    public AnimationManager AnimationManager => _animationManager;


    private StateManager _stateManager;

    void Awake()
    {
        _animationManager = GetComponent<AnimationManager>();
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = playerStats.MovementSpeed;
        _stateManager = new(this);
    }

    void Update()
    {
        _stateManager.Update();
    }
}
