
using System;
using UnityEngine;

public class CommandFactory
{
    public static ICommand CreateSpellCommand(
        Spell spell,
        Transform casterTransform,
        Func<float> animationFunc)
    {
        return spell switch
        {
            FireSpell fireSpell => new FireSpellCommand(
                casterTransform,
                fireSpell,
                animationFunc),

            _ => null
        };
    }

    public static ICommand CreateAttackCommand(
        Attack attack,
        Transform attackerTransform,
        Func<float> animationFunc,
        IDamagable target
    )
    {
        return new AttackCommand(
            attackerTransform,
            attack,
            animationFunc,
            target
        );
    }
}
