using System;
using UnityEngine;

public class SceneRules : MonoBehaviour
{
    public static SceneRules Instance { get; private set; }

    [SerializeField] private GameObject RuleMenu;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        RuleMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RuleMenu.SetActive(false);
            Time.timeScale = 1f;
        }
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
            GameOver.Instance.Lost();
        }
    }

    //must finish boss with mele
    public void SecondRule(IDamagable caster, IDamagable target, Attack attack)
    {
        bool isPlayer = caster is DamagablePlayer;
        if (isPlayer && target is DamagableBoss boss)
        {
            if (boss.IsDead())
            {
                if (attack == null)
                {
                    GameOver.Instance.Lost();
                    return;
                }
                GameOver.Instance.Won();
            }
        }
    }

    private int counter = 0;
    public void ThirdRule(IDamagable caster, IDamagable target, Attack attack)
    {
        bool isMinion = caster is DamagableMinion;
        bool isPlayer = target is DamagablePlayer;
        bool attacked = attack != null;
        if (isMinion && isPlayer && attacked && ++counter >= 5)
        {
            GameOver.Instance.Lost();
        }
    }
}
