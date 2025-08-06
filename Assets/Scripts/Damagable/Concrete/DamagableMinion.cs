using UnityEngine;

[RequireComponent(typeof(MinionController))]
public class DamagableMinion : BaseDamagable
{

    private MinionController _minionController;

    protected override void SubclassAwake()
    {
        _minionController = GetComponent<MinionController>();
    }

    public override void TakeDamage(uint damage)
    {
        currHealth -= damage;

        if (currHealth <= 0)
        {
            _minionController.ReleaseMe();
        }
    }
}
