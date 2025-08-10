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
        var casterPosition = _casterTransform.position;
        if (Vector3.Distance(casterPosition, _center) > _iceSpell.MaxDistance)
        {
            return;
        }
        float duration = _animationFunc();
        await Awaitable.WaitForSecondsAsync(duration);
        FallingEffect();
    }



    private void FallingEffect()
    {
        var duration = _iceSpell.NumOfCycles * _iceSpell.WaitBetweenCycles;
        var center = new Vector3(_center.x, 0f, _center.z);
        var effect = UnityEngine.Object.Instantiate(_iceSpell.FallingEffect, center, Quaternion.identity);
        UnityEngine.Object.Destroy(effect.gameObject, 2f);
    }
    private void HitEffect(IDamagable damagable)
    {
        var effect = UnityEngine.Object.Instantiate(_iceSpell.HitEffect, damagable.GetPosition(), Quaternion.identity);
        UnityEngine.Object.Destroy(effect, 1f);
    }
}
