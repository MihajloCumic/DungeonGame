using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MinionController))]
public class DamagableMinion : BaseDamagable
{
    private MinionController _minionController;

    private Renderer[] _renderers;
    private Color _originalColor = Color.black;

    protected override void SubclassAwake()
    {
        _minionController = GetComponent<MinionController>();
        _renderers= GetComponentsInChildren<Renderer>(true);
    }

    public override void TakeDamage(uint damage)
    {
        bool isDead = false;
        currHealth -= (int)damage;
        SetVisualMarker();

        if (IsDead())
        {
            _minionController.ReleaseMe();
            SetColor(_originalColor);
            isDead = true;
        }
        damageEvent.Trigger(this, damage, isDead);
    }

    

    public override void SetVisualMarker()
    {
        StartCoroutine(ChangeColor());
    }

    private IEnumerator ChangeColor()
    {
        SetColor(Color.red);
        yield return new WaitForSeconds(0.2f);
        SetColor(_originalColor);
    }

    private void SetColor(Color color)
    {
        foreach(Renderer r in _renderers)
        {
            r.material.color = color;
        }
    }
}
