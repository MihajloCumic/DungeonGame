using System;

public class DamageArgs : EventArgs
{
    public uint DamageTaken { get; private set; }
    public bool IsDead { get; private set; }

    public DamageArgs(uint damageTaken, bool isDead)
    {
        DamageTaken = damageTaken;
        IsDead = isDead;
    }
}
