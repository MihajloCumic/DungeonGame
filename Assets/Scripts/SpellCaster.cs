using Unity.VisualScripting;
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
        if (Input.GetKey(KeyCode.Q) && Input.GetMouseButton(0))
        {
            _indicator.transform.position = transform.position;
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Vector3 direction = hit.point - _indicator.transform.position;
                direction.y = 0;
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                _indicator.transform.rotation = targetRotation;
                
            }
            _indicator.SetActive(true);
            // _command.Execute();
            return;
        }
        _indicator.SetActive(false);
    }
}
