
using UnityEngine;

public class CommandFactory
{
    public static ICommand Create(
        Transform casterTransform,
        Spell spell,
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
}
