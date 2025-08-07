using System.Threading.Tasks;
using UnityEngine;

public class FireSpellCommand : ICommand
{
    private readonly Transform _playerTransform;
    private readonly FireSpell _fireSpell;

    public FireSpellCommand(Transform playerTransform, FireSpell fireSpell)
    {
        _playerTransform = playerTransform;
        _fireSpell = fireSpell;
    }

    public Task Execute()
    {
        var origin = _playerTransform.position;
        var direction = _playerTransform.forward;
        var maxDistance = _fireSpell.MaxDistance;
        Debug.DrawRay(origin + new Vector3(0, 5f, 0), direction * maxDistance, Color.rebeccaPurple);
        if (Physics.Raycast(origin, direction, out RaycastHit hit, maxDistance))
        {
            if (hit.transform.TryGetComponent(out IDamagable damagable))
            {
                damagable.TakeDamage(_fireSpell.BaseDamage);
            }

        }

        return Task.CompletedTask;
    }
}
