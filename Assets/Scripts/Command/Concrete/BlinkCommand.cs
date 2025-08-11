using System;
using System.Threading.Tasks;
using UnityEngine;

public class BlinkCommand : ICommand
{
    private readonly Transform _casterTransform;
    private readonly BlinkSpell _blinkSpell;

    public BlinkCommand(Transform casterTransform, BlinkSpell blinkSpell)
    {
        _casterTransform = casterTransform;
        _blinkSpell = blinkSpell;
    }
    public async Task Execute()
    {
        var delay = _blinkSpell.Delay;
        await Awaitable.WaitForSecondsAsync(delay);
        var origin = _casterTransform.position;
        var direction = _casterTransform.forward;
        var distance = _blinkSpell.Distance;
        if (Physics.Raycast(origin, direction, out RaycastHit hit, distance))
        {
            if (hit.transform.TryGetComponent(out IDamagable damagable))
            {
                var distanceFromDamagable = Vector3.Distance(origin, damagable.GetPosition());
                float offset = 0f;
                if (Math.Abs(distance - distanceFromDamagable) < 0.2f)
                {
                    offset = 0.2f;
                }
                RenderEffect();
                _casterTransform.position = hit.point - _casterTransform.forward * offset;
                Debug.Log("Blink");
                return;
            }
        }
        RenderEffect();
        _casterTransform.position = hit.point - _casterTransform.forward;
        Debug.Log("Blink");
    }

    private void RenderEffect()
    {
        var effect = UnityEngine.Object.Instantiate(_blinkSpell.Effect, _casterTransform.position, Quaternion.identity);
        UnityEngine.Object.Destroy(effect.gameObject, 1f);
    }
}
