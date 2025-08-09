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
        _animator = GetComponentInChildren<Animator>();
    }

    void OnEnable()
    {
        _animator.CrossFade("Run", 0.1f);
    }
    void Update()
    {
        Rotate(transform.position, PlayerTransform);
        if (IsInRange())
        {
            
        }
        _agent.SetDestination(PlayerTransform.position);
    }
    private bool IsInRange()
    {
        var distance = Vector3.Distance(transform.position, PlayerTransform.position);
        return distance <= 2f;
    }
    private void Rotate(Vector3 minionPosition, Transform playerTransform)
    {
        var direction = minionPosition - playerTransform.position;
        Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = rotation;
    }
}
