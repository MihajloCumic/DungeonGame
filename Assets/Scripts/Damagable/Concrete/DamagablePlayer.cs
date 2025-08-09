using UnityEngine;

public class DamagablePlayer: BaseDamagable
{
    public override void TakeDamage(uint damage)
    {
        currHealth -= (int)damage;
        bool isDead = IsDead();
        if (isDead)
        {
            Time.timeScale = 0f;
        }
        damageEvent.Trigger(this, damage, isDead);
    }
    
}
