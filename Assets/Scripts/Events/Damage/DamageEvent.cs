
public class DamageEvent
{
    public delegate void DamageDelegate(IDamagable sender, DamageArgs damageArgs);
    public delegate void HealDelegate(IDamagable sender, uint healAmount);

    public event DamageDelegate DamageHandler;
    public event HealDelegate HealHandler;


    public void Trigger(IDamagable sender, uint damageTaken, bool isDead)
    {
        DamageHandler?.Invoke(sender, new DamageArgs(damageTaken, isDead));
    }
    public void Trigger(IDamagable sender, uint healAmount)
    {
        HealHandler?.Invoke(sender, healAmount);
    }
}
