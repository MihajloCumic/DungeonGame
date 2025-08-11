using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BossController))]
public class DamagableBoss : BaseDamagable
{
    private BossController _minionController;

    private Renderer[] _renderers;
    private readonly Color _originalColor = Color.black;

    protected override void SubclassAwake()
    {
        _minionController = GetComponent<BossController>();
        _renderers= GetComponentsInChildren<Renderer>(true);
    }

    public override void TakeDamage(uint damage)
    {
        bool isDead = false;
        currHealth -= (int)damage;
        SetVisualMarker();

        if (IsDead())
        {
            SetColor(_originalColor);
            isDead = true;
            gameObject.SetActive(false);
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
