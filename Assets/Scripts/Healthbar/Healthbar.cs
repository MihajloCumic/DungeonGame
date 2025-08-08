using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(IDamagable))]
public class Healthbar : MonoBehaviour
{
    private uint _currHealth;
    private uint _maxHealth;
    private Slider _slider;

    void Awake()
    {
        _slider = GetComponentInChildren<Slider>();
        if (_slider == null)
        {
            Debug.Log("No Slider attached to child.");
            Destroy(this.gameObject);
            return;
        }
    }

    void Onable()
    {

    }
    void Start()
    {
        var damagable = GetComponent<IDamagable>();
        damagable.Subscribe(RegisterDamageTaken);

        int currHealth = damagable.GetCurrHealth();
        _currHealth = currHealth < 0 ? 0 : (uint)currHealth;
        _slider.minValue = 0;
        _slider.maxValue = _currHealth;
        _slider.value = _currHealth;
    }

    public void RegisterDamageTaken(IDamagable sender, DamageArgs damageArgs)
    {
        uint damageTaken = damageArgs.DamageTaken;
        bool isDead = damageArgs.IsDead;

        if (isDead)
        {
            _currHealth = 0;
            _slider.value = 0f;
            return;
        }

        _currHealth -= damageTaken;
        _slider.value -= damageTaken;
    }
}
