
using System;
using UnityEngine;

public class CommandFactory
{
    public static ICommand CreateCommand(
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
            BlinkSpell blinkSpell => new BlinkCommand(
                casterTransform,
                blinkSpell
            ),
            _ => null
        };
    }

    public static ICommand CreateCommand(
        Spell spell,
        Transform transform,
        Func<float> animationFunc,
        IDamagable player
    )
    {
        return spell switch
        {
            ExplodePlayerSpell explodeSpell => new ExplodePlayerCommand(
                transform,
                player,
                explodeSpell,
                animationFunc
            ),
            BurnAroundSpell burnSpell => new BurnAroundCommand(
                transform,
                burnSpell,
                animationFunc
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
