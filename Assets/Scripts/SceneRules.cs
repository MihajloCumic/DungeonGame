using System;
using UnityEngine;

public class SceneRules : MonoBehaviour
{
    public static SceneRules Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void CheckRules(IDamagable caster, IDamagable target, Spell spell, Attack attack)
    {
        FirstRule(caster, target, spell);
        SecondRule(caster, target, attack);
        ThirdRule(caster, target, attack);
    }

    //cant use firespell on minions
    public void FirstRule(IDamagable caster, IDamagable target, Spell spellUsed)
    {
        bool isPlayer = caster is DamagablePlayer;
        bool isMinion = target is DamagableMinion;
        bool isFireSpell = spellUsed is FireSpell;

        if (isPlayer && isMinion && isFireSpell)
        {
            Debug.Log("Attacked minion with firespell.");
            Time.timeScale = 0f;
        }
    }

    //must finish boss with mele
    public void SecondRule(IDamagable caster, IDamagable target, Attack attack)
    {
        bool isPlayer = caster is DamagablePlayer;
        Debug.Log("Is player:" + isPlayer);
        if (isPlayer && target is DamagableMinion damagableMinion)
        {
            if (damagableMinion.TryGetComponent(out BossController bossController))
            {
                Debug.Log("It is a boss");
                if (bossController.IsDead() && attack == null)
                {
                    Debug.Log("Did not kill boss with mele");
                    Time.timeScale = 0f;
                }
            }
        }
    }

    private static readonly Func<bool> counter = MinionHitCounter();
    //cant get hit more than 5 times bu minions
    public void ThirdRule(IDamagable caster, IDamagable target, Attack attack)
    {
        bool isMinion = caster is DamagableMinion;
        bool isPlayer = target is DamagablePlayer;
        bool attacked = attack != null;
        if (isMinion && isPlayer && attacked && counter())
        {
            Debug.Log("Got hit by a minion more than 5 times");
            Time.timeScale = 0f;
        }
    }
    public static Func<bool> MinionHitCounter()
    {
        int cnt = 0;
        return () =>
        {
            return ++cnt > 5;
        };
    }

    

}
