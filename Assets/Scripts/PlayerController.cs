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
    [SerializeField] private MoveIndicator moveIndicator;
    public MoveIndicator MoveIndicator => moveIndicator;
    private NavMeshAgent _agent;
    public NavMeshAgent Agent => _agent;
    private AnimationManager _animationManager;
    public AnimationManager AnimationManager => _animationManager;
    private StateManager _stateManager;
    private readonly CooldownEvent _cooldownEvent = new();
    public CooldownEvent CooldownEvent => _cooldownEvent;

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

    public void Subscribe(CooldownEvent.CooldownDelegate handler)
    {
        _cooldownEvent.CooldownHandler += handler;
    }
}
