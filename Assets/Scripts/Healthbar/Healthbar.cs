using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(IDamagable))]
public class Healthbar : MonoBehaviour
{
    private uint _currHealth;
    private Slider _slider;
    private IDamagable _damagable;

    void Awake()
    {
        _slider = GetComponentInChildren<Slider>();
        _damagable = GetComponent<IDamagable>();
        _damagable.Subscribe(RegisterDamageTaken);
    }
    
    void OnEnable()
    {
        var maxHealth = _damagable.GetMaxHealth();
        var currHealth = maxHealth < 0 ? 0 : maxHealth;
        _currHealth = currHealth;
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
