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

    public int GetCurrHealth()
    {
        return currHealth;
    }

    public void Subscribe(DamageEvent.DamageDelegate handler)
    {
        damageEvent.DamageHandler += handler;
    }
}
