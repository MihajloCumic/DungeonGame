using UnityEngine;

public abstract class BaseStats : ScriptableObject
{
       [SerializeField] protected uint maxHealth;
       [SerializeField] protected float movementSpeed;

       public uint MaxHealth
       {
              get { return maxHealth; }
       }

       public float MovementSpeed
       {
              get { return movementSpeed; }
       }
}
