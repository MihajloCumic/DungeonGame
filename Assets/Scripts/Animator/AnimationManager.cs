using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    private Animator _animator;
    private static readonly int idleHash = Animator.StringToHash("Idle");
    private static readonly int runHash = Animator.StringToHash("Run");
    private static readonly int meleHash = Animator.StringToHash("Mele");
    private static readonly int castHash = Animator.StringToHash("Cast");
    private const float _crossFade = 0.1f;

    private readonly Dictionary<int, float> _animationDuration = new()
    {
        {idleHash, 1.733f},
        {runHash, 0.667f},
        {meleHash, 0.867f},
        {castHash, 0.5f}//0.73
    };

    void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    public float Idle() => PlayAnimation(idleHash);
    public float Run() => PlayAnimation(runHash);
    public float Mele() => PlayAnimation(meleHash);
    public float Cast() => PlayAnimation(castHash);


    private float PlayAnimation(int animationHash)
    {
        _animator.CrossFade(animationHash, _crossFade);
        return _animationDuration[animationHash];
    }
}
