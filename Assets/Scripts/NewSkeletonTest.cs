using UnityEngine;

[RequireComponent(typeof(AnimationManager))]
public class NewSkeletonTest : MonoBehaviour
{
    private AnimationManager _am;

    void Awake()
    {
        _am = GetComponent<AnimationManager>();
    }

    void Start()
    {
        _am.MinionAttack();
    }
}
