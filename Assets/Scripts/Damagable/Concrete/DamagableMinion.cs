using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MinionController))]
public class DamagableMinion : BaseDamagable
{
    private MinionController _minionController;

    private Renderer _renderer;
    private Color _originalColor;

    protected override void SubclassAwake()
    {
        _minionController = GetComponent<MinionController>();
        _renderer = GetComponentInChildren<Renderer>();
        _originalColor = _renderer.material.color;
    }

    public override void TakeDamage(uint damage)
    {
        bool isDead = false;
        currHealth -= (int)damage;
        SetVisualMarker();

        if (IsDead())
        {
            _minionController.ReleaseMe();
            _renderer.material.color = _originalColor;
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
        _renderer.material.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        _renderer.material.color = _originalColor;
    }
}
