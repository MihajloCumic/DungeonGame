using UnityEngine;

public abstract class BaseDamagable : MonoBehaviour, IDamagable
{
    [SerializeField] protected BaseStats baseStats;
    protected uint currHealth;

    void Awake()
    {
        currHealth = baseStats.MaxHealth;
        SubclassAwake();
    }

    protected virtual void SubclassAwake(){}

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public bool IsDead()
    {
        return currHealth <= 0 || !gameObject.activeInHierarchy;
    }

    public abstract void TakeDamage(uint damage);
    
}
