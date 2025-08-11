using System.Threading.Tasks;
using UnityEngine;

public class HealCommand : ICommand
{
    private readonly BaseDamagable _damagable;
    private readonly HealSpell _healSpell;

    public HealCommand(BaseDamagable damagable, HealSpell healSpell)
    {
        _damagable = damagable;
        _healSpell = healSpell;
    }

    public async Task Execute()
    {
        float delay = _healSpell.Delay;
        await Awaitable.WaitForSecondsAsync(delay);
        HealEffet();
        _damagable.Heal(_healSpell.HealAmount);
    }

    private void HealEffet()
    {
        var effect = UnityEngine.Object.Instantiate(_healSpell.Effect, _damagable.GetPosition(), Quaternion.identity);
        UnityEngine.Object.Destroy(effect.gameObject, 1f);
    }
}
