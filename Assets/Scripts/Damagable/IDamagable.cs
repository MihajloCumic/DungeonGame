using UnityEngine;

public interface IDamagable
{
    void TakeDamage(uint damage);
    bool IsDead();
    Vector3 GetPosition();
}
