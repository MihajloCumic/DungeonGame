using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    private Animator _animator;
    public static readonly int idleHash = Animator.StringToHash("Idle");
    private static readonly int runHash = Animator.StringToHash("Run");
    private static readonly int meleHash = Animator.StringToHash("Mele");
    private static readonly int castHash = Animator.StringToHash("Cast");
    private static readonly int minionAttackHash = Animator.StringToHash("MinionAttack");
    private static readonly int bossCastHash = Animator.StringToHash("BossCast");
    private const float _crossFade = 0.1f;

    private readonly Dictionary<int, float> _animationDuration = new()
    {
        {idleHash, 1.733f},
        {runHash, 0.667f},
        {meleHash, 0.6f},//0.867
        {castHash, 0.5f},//0.73
        {minionAttackHash, 1.5f},
        {bossCastHash, 1.2f}
    };

    void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    public float Idle() => PlayAnimation(idleHash);
    public float Run() => PlayAnimation(runHash);
    public float Mele() => PlayAnimation(meleHash);
    public float Cast() => PlayAnimation(castHash);
    public float MinionAttack() => PlayAnimation(minionAttackHash);
    public float BossCast() => PlayAnimation(bossCastHash);


    private float PlayAnimation(int animationHash)
    {
        _animator.CrossFade(animationHash, _crossFade);
        return _animationDuration[animationHash];
    }
}
