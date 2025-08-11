using UnityEngine;

public abstract class BaseDamagable : MonoBehaviour, IDamagable
{
    [SerializeField] protected BaseStats baseStats;
    protected int currHealth;

    protected DamageEvent damageEvent = new();

    void Awake()
    {
        currHealth = (int)baseStats.MaxHealth;
        SubclassAwake();
    }

    void OnEnable()
    {
        currHealth = (int)baseStats.MaxHealth;
    }

    protected virtual void SubclassAwake() { }


    public abstract void TakeDamage(uint damage);
    public virtual void Heal(uint healAmount)
    {
        currHealth += (int)healAmount;
        damageEvent.Trigger(this, healAmount);
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public bool IsDead()
    {
        return currHealth <= 0 || !gameObject.activeInHierarchy;
    }

    public int GetCurrHealth()
    {
        return currHealth;
    }

    public void Subscribe(DamageEvent.DamageDelegate handler)
    {
        damageEvent.DamageHandler += handler;
    }

    public void Subscribe(DamageEvent.HealDelegate handler)
    {
        damageEvent.HealHandler += handler;
    }

    public uint GetMaxHealth()
    {
        return baseStats.MaxHealth;
    }

    public virtual void SetVisualMarker()
    {

    }
}
