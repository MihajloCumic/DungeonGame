using System.Threading.Tasks;
using UnityEngine;

public class FireSpellCommand : ICommand
{
    private readonly Transform _casterTransform;
    private readonly FireSpell _fireSpell;

    public FireSpellCommand(Transform casterTransform, FireSpell fireSpell)
    {
        _casterTransform = casterTransform;
        _fireSpell = fireSpell;
    }

    public Task Execute()
    {
        var origin = _casterTransform.position;
        var direction = _casterTransform.forward;
        var maxDistance = _fireSpell.MaxDistance;

        if (Physics.Raycast(origin, direction, out RaycastHit hit, maxDistance))
        {
            if (hit.transform.TryGetComponent(out IDamagable damagable))
            {
                damagable.TakeDamage(_fireSpell.BaseDamage);
            }

        }

        return Task.CompletedTask;
    }

    // private void RenderLine(Vector3 origin, Vector3 direction, float distance)
    // {
    //     var endPoint = origin + direction * distance;
    //     _casterLineRenderer.startWidth = 1f;
    //     _casterLineRenderer.endWidth = 0.1f;
    //     _casterLineRenderer.SetPosition(0, origin);
    //     _casterLineRenderer.SetPosition(1, endPoint);
    // }
}
