using System;
using System.Threading.Tasks;
using UnityEngine;

public class IceShardsCommand : ICommand
{
    private readonly Transform _casterTransform;
    private readonly IceSpell _iceSpell;
    private readonly Func<float> _animationFunc;
    private readonly Vector3 _center;

    public IceShardsCommand(Transform casterTransform, IceSpell iceSpell, Func<float> animationFunc, Vector3 center)
    {
        _casterTransform = casterTransform;
        _iceSpell = iceSpell;
        _animationFunc = animationFunc;
        _center = center;
    }
    public async Task Execute()
    {
        float duration = _animationFunc();
        await Awaitable.WaitForSecondsAsync(duration);
        FallingEffect();
    }



    private void FallingEffect()
    {
        var duration = _iceSpell.NumOfCycles * _iceSpell.WaitBetweenCycles;
        var center = _center - new Vector3(0, _center.y, 0);
        var effect = UnityEngine.Object.Instantiate(_iceSpell.FallingEffect, center, Quaternion.identity);
        effect.transform.localScale = Vector3.one * (_iceSpell.SpellRadius * 2f);
        UnityEngine.Object.Destroy(effect, duration);

    }
    private void HitEffect(IDamagable damagable)
    {
        var effect = UnityEngine.Object.Instantiate(_iceSpell.HitEffect, damagable.GetPosition(), Quaternion.identity);
        UnityEngine.Object.Destroy(effect, 1f);
    }
}
