using UnityEngine;

public class DamagablePlayer: BaseDamagable
{
    public override void TakeDamage(uint damage)
    {
        currHealth -= (int)damage;
        if (IsDead())
        {
            gameObject.SetActive(false);
        }
    }
    
}
