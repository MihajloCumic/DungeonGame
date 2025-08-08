using UnityEngine;

public abstract class BaseStats : ScriptableObject
{
       [SerializeField] protected uint maxHealth;
       [SerializeField] protected float movementSpeed;

       public uint MaxHealth => maxHealth;
       public float MovementSpeed => movementSpeed;
}
