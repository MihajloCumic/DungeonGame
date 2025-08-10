using System;
using System.Threading.Tasks;
using UnityEngine;

public class ExplodePlayerCommand:ICommand 
{
    private readonly Transform _casterTransform;
    private readonly IDamagable _damagable;
    private readonly ExplodePlayerSpell _explodeSpell;
    private readonly Func<float> _animationFunc;
    private readonly int playerLayer = LayerMask.NameToLayer("PlayerLayer");

    public ExplodePlayerCommand(Transform casterTransform, IDamagable damagable, ExplodePlayerSpell explodeSpell, Func<float> animationFunc)
    {
        _casterTransform = casterTransform;
        _damagable = damagable;
        _explodeSpell = explodeSpell;
        _animationFunc = animationFunc;
    }
    public async Task Execute()
    {
        _animationFunc();
        for (int i = 0; i < _explodeSpell.NumOfCycles; i++)
        {
            await Explode();
        }
    }

    private async Task Explode()
    {
        var damagablePosition = _damagable.GetPosition();
        ExplodeIndicatorEffect(_explodeSpell.Delay, damagablePosition);
        await Awaitable.WaitForSecondsAsync(_explodeSpell.Delay);
        LayerMask layerMask = 1 << playerLayer;
        Collider[] hits = Physics.OverlapSphere(damagablePosition, _explodeSpell.Radius, layerMask);
        foreach (Collider hit in hits)
        {
            if (hit.transform.TryGetComponent(out IDamagable damagable))
            {
                damagable.TakeDamage(_explodeSpell.BaseDamage);
            }
        }
        ExplodeEffect(damagablePosition);
    }

    private void ExplodeIndicatorEffect(float duration, Vector3 position)
    {
        var casterPostion = _casterTransform.position;
        var center = new Vector3(position.x, 0f, position.z);
        var effect = UnityEngine.Object.Instantiate(_explodeSpell.IndicatorEffect, center, Quaternion.identity);
        var g = effect.gameObject;
        g.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        UnityEngine.Object.Destroy(effect.gameObject, duration);
    }
    private void ExplodeEffect(Vector3 position)
    {
        var effect = UnityEngine.Object.Instantiate(_explodeSpell.Effect, position, Quaternion.identity);
        UnityEngine.Object.Destroy(effect.gameObject, 1f);
    }

}
