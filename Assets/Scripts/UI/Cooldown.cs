using System.Collections;
using TMPro;
using UnityEngine;

public class Cooldown : MonoBehaviour
{
    [SerializeField] private GameObject cover;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Spell _spell;
    

    [SerializeField] PlayerController playerController;

    void Awake()
    {
        playerController.Subscribe(StartCooldown);
    }

    public void StartCooldown(Spell spell)
    {
        if (spell.GetType() != _spell.GetType()) return;
        StartCoroutine(CountDown(_spell.Cooldown));
    }

    private IEnumerator CountDown(uint cooldown)
    {
        cover.SetActive(true);
        text.gameObject.SetActive(true);
        while (cooldown > 0)
        {
            text.SetText(cooldown + "");
            yield return new WaitForSeconds(1f);
            --cooldown;
        }
        cover.SetActive(false);
        text.gameObject.SetActive(false);
    }
}
