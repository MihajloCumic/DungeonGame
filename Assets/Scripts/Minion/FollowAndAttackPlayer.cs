using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class FollowAndAttackPlayer : MonoBehaviour
{
    public Transform PlayerTransform { private get; set; }
    private NavMeshAgent _agent;
    private Animator _animator;

    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = 0f;
        _animator = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        _animator.CrossFade("Run", 0.1f);
    }
    void Update()
    {
        if (PlayerTransform != null)
        {
            Rotate(transform.position, PlayerTransform);
            _agent.SetDestination(PlayerTransform.position);
        }
        
    }
    private void Rotate(Vector3 minionPosition, Transform playerTransform)
    {
        var direction = minionPosition - playerTransform.position;
        Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = rotation;
    }
}
