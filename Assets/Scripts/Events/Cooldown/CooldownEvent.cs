
public class CooldownEvent
{
    public delegate void CooldownDelegate(Spell spell);
    public event CooldownDelegate CooldownHandler;

    public void Trigger(Spell spell)
    {
        CooldownHandler?.Invoke(spell);
    }
}
