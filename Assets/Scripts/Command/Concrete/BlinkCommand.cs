using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

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
        RenderEffect(5f);
        await Awaitable.WaitForSecondsAsync(delay);
        var blinkPoint = _casterTransform.position + _casterTransform.forward * _blinkSpell.Distance;
        if (NavMesh.SamplePosition(blinkPoint, out NavMeshHit hit, 1.0f, NavMesh.AllAreas))
        {
            if (_casterTransform.TryGetComponent(out NavMeshAgent agent))
            {
                agent.Warp(hit.position);
                RenderEffect(0.5f);
            }
        }
    }

    private void RenderEffect(float destroyAfter)
    {
        var effect = UnityEngine.Object.Instantiate(_blinkSpell.Effect, _casterTransform.position, Quaternion.identity);
        UnityEngine.Object.Destroy(effect.gameObject, destroyAfter);
    }
}
