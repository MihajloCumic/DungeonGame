
using UnityEngine;

public class CommandFactory
{
    public static ICommand CreateSpellCommand(
        Spell spell,
        Transform casterTransform,
        AnimationManager animationManager)
    {
        return spell switch
        {
            FireSpell fireSpell => new FireSpellCommand(
                casterTransform,
                fireSpell,
                animationManager),

            _ => null
        };
    }

    public static ICommand CreateAttackCommand(
        Attack attack,
        Transform attackerTransform,
        AnimationManager animationManager,
        IDamagable target
    )
    {
        return new AttackCommand(
            attackerTransform,
            attack,
            animationManager,
            target
        );
    }
}
