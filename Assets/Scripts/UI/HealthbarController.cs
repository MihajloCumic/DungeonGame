using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class PlaterHealthbar : MonoBehaviour
{
    [SerializeField] private BaseDamagable damagable;
    private uint _currHealth;
    private Slider _slider;

    void Awake()
    {
        _currHealth = damagable.GetMaxHealth();
        _slider = GetComponent<Slider>();
        _slider.minValue = 0;
        _slider.maxValue = _currHealth;
        _slider.value = _currHealth;
        damagable.Subscribe(RegisterDamageTaken);
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
