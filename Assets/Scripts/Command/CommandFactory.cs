
using System;
using UnityEngine;

public class CommandFactory
{
    public static ICommand CreateSpellCommand(
        Spell spell,
        Transform casterTransform,
        Func<float> animationFunc,
        Vector3 mouseHitPosition
        )
    {
        return spell switch
        {
            FireSpell fireSpell => new FireSpellCommand(
                casterTransform,
                fireSpell,
                animationFunc),
            IceSpell iceSpell => new IceShardsCommand(
                casterTransform,
                iceSpell,
                animationFunc,
                mouseHitPosition
            ),
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
