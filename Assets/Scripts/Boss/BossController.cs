using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AnimationManager))]
public class BossController : MonoBehaviour, IDamagable
{
    [SerializeField] private BossStats bossStats;
    [SerializeField] private List<Spell> spells;
    private AnimationManager _animationManager;
    private int _currHealth;

    void Awake()
    {
        _currHealth = (int)bossStats.MaxHealth;
        _animationManager = GetComponent<AnimationManager>();
    }

    void Start()
    {
        _animationManager.Idle();
    }
    public int GetCurrHealth()
    {
        return _currHealth;
    }

    public uint GetMaxHealth()
    {
        return bossStats.MaxHealth;
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public bool IsDead()
    {
        return _currHealth <= 0 || !gameObject.activeInHierarchy;
    }

    public void SetVisualMarker()
    {
        return;
    }

    public void Subscribe(DamageEvent.DamageDelegate handler)
    {
        return;
    }

    public void TakeDamage(uint damage)
    {
        _currHealth -= (int)damage;
        if (_currHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }

}
