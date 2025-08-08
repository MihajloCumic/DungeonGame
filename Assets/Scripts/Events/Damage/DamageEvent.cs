
public class DamageEvent
{
    public delegate void DamageDelegate(IDamagable sender, DamageArgs damageArgs);

    public event DamageDelegate DamageHandler;

    public void Trigger(IDamagable sender, uint damageTaken, bool isDead)
    {
        DamageHandler?.Invoke(sender, new DamageArgs(damageTaken, isDead));
    }
}
