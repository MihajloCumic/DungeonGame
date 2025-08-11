using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(AnimationManager))]
public class BossController : MonoBehaviour
{
    [SerializeField] private BossStats bossStats;
    [SerializeField] private List<Spell> spells;
    [SerializeField] private BaseDamagable player;
    [SerializeField] private BossAttack bossAttack;
    private AnimationManager _animationManager;
    private float _lastAttackTime = -Mathf.Infinity;
    private volatile bool _lock = false;

    void Awake()
    {
        _animationManager = GetComponent<AnimationManager>();
    }

    void Start()
    {
        _animationManager.Idle();
        StartCoroutine(BossLogic());

    }

    void Update()
    {
        Rotate();
        if (IsPlayerInRange() && IsAttackCooldownUp() && !_lock)
        {
            var command = CommandFactory.CreateAttackCommand(
                bossAttack,
                transform,
                _animationManager.BossAttack,
                player
            );
            _lock = true;
            ExecuteBlockingCommand(command);
        }
    }

    private async void ExecuteBlockingCommand(ICommand command)
    {
        await command.Execute();
        _lastAttackTime = Time.time;
        _lock = false;
        _animationManager.Idle();
    }



    private IEnumerator InitialWait()
    {
        yield return new WaitForSeconds(bossStats.StartDelay);
    }

    private IEnumerator BossLogic()
    {
        yield return InitialWait();
        while (gameObject.activeInHierarchy)
        {
            float lower = bossStats.LowerAttackInterval;
            float upper = bossStats.UpperAttackInterval;
            float attackWait = Random.Range(lower, upper);
            yield return new WaitForSeconds(attackWait);

            while (_lock)
            {
                yield return null;
            }

            int listCnt = spells.Count;
            int spellNum = Random.Range(0, listCnt);
            Spell spell = spells[spellNum];
            ICommand spellCommand = CommandFactory.CreateCommand(
                spell,
                transform,
                _animationManager.BossCast,
                player
            );
            Task task = spellCommand.Execute();
            
            while (!task.IsCompleted)
            {
                yield return null;
            }
        }
    }

    private void Rotate()
    {
        var direction = (player.transform.position - transform.position).normalized;
        direction.y = 0.1f;
        Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = rotation;
    }
    private bool IsPlayerInRange()
    {
        var distance = Vector3.Distance(transform.position, player.transform.position);
        return distance <= bossAttack.MaxDistance;
    }

    private bool IsAttackCooldownUp()
    {
        return Time.time - _lastAttackTime >= bossAttack.Cooldown;
    }

}
