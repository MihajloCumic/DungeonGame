using UnityEngine;

public class SpellCaster : MonoBehaviour
{

    private ICommand _command;
    [SerializeField] private FireSpell firespellSo;
    private GameObject _indicator;

    void Awake()
    {
        _command = new FireSpellCommand(transform, firespellSo);
    }

    void Start()
    {
        _indicator = Instantiate(firespellSo.Indicator);
        _indicator.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            if (Input.GetMouseButton(0))
            {
                _indicator.SetActive(true);
                var mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                firespellSo.DrawIndicator(transform.position, mouseRay, _indicator);
            }
            if (Input.GetMouseButtonUp(0))
            {
                _command.Execute();
            }
            return;
        }       
        _indicator.SetActive(false);
    }
}
