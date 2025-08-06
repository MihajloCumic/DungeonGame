using UnityEngine;

public class DamagablePlayer: BaseDamagable
{
    public override void TakeDamage(uint damage)
    {
        currHealth -= damage;
        if (currHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }
    
}
