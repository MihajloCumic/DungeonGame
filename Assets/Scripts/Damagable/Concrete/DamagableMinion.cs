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
        bool isDead = false;
        currHealth -= (int)damage;

        if (IsDead())
        {
            _minionController.ReleaseMe();
            isDead = true;
        }
        damageEvent.Trigger(this, damage, isDead);
    }
}
