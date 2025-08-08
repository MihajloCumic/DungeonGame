using UnityEngine;

public abstract class BaseDamagable : MonoBehaviour, IDamagable
{
    [SerializeField] protected BaseStats baseStats;
    protected uint currHealth;

    protected DamageEvent damageEvent = new();

    void Awake()
    {
        currHealth = baseStats.MaxHealth;
        SubclassAwake();
    }

    protected virtual void SubclassAwake() { }


    public abstract void TakeDamage(uint damage);

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public bool IsDead()
    {
        return currHealth <= 0 || !gameObject.activeInHierarchy;
    }

    public uint GetCurrHealth()
    {
        return currHealth;
    }

    public void Subscribe(DamageEvent.DamageDelegate handler)
    {
        damageEvent.DamageHandler += handler;
    }

    
}
