using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private Vector3 _offset;
    private const float _transitionSpeed = 0.1f;

    void Awake()
    {
        _offset = transform.position - player.transform.position;
    }
    void LateUpdate()
    {
        var playerPos = player.transform.position;
        var newPosition = playerPos + _offset;
        var transition = Vector3.Lerp(transform.position, newPosition, _transitionSpeed);

        transform.position = transition;
    }
}
