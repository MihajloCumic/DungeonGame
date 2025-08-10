using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(AnimationManager))]
public class BossController : MonoBehaviour, IDamagable
{
    [SerializeField] private BossStats bossStats;
    [SerializeField] private List<Spell> spells;
    [SerializeField] private BaseDamagable player;
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
        Spell spell = spells[1];
        ICommand spellCommand = CommandFactory.CreateCommand(
            spell,
            transform,
            _animationManager.BossCast,
            player
        );
        Execute(spellCommand);
        // StartCoroutine(BossLogic());

    }

    private async void Execute(ICommand command)
    {
        await command.Execute();
        _animationManager.Idle();
    }

    private IEnumerator InitialWait()
    {
        yield return new WaitForSeconds(bossStats.StartDelay);
    }

    private IEnumerator BossLogic()
    {
        yield return InitialWait();
        while (!IsDead())
        {
            float lower = bossStats.LowerAttackInterval;
            float upper = bossStats.UpperAttackInterval;
            float attackWait = Random.Range(lower, upper);
            yield return new WaitForSeconds(attackWait);

            int listCnt = spells.Count;
            int spellNum = Random.Range(0, listCnt);
            Spell spell = spells[0];
            ICommand spellCommand = CommandFactory.CreateCommand(
                spell,
                transform,
                _animationManager.BossCast,
                Vector3.zero
            );
            // Task task = spellCommand.Execute();
            // while (!task.IsCompleted)
            // {
            //     yield return null;
            // }
            spellCommand.Execute();
            
        }
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
