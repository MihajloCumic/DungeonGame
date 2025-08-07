using UnityEngine;

[RequireComponent(typeof(AnimationManager))]
public class SpellCaster : MonoBehaviour
{

    private ICommand _command;
    [SerializeField] private FireSpell firespellSo;
    private GameObject _indicator;
    private AnimationManager _animationManager;

    void Awake()
    {
        _animationManager = GetComponent<AnimationManager>();
        _command = new FireSpellCommand(transform, firespellSo, _animationManager);
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
                if (Physics.Raycast(mouseRay, out RaycastHit hit))
                {
                    if (hit.transform.TryGetComponent(out PlayerController playerController))
                    {
                        _indicator.SetActive(false);
                        return;
                    }
                    Rotate(hit.point);
                    firespellSo.DrawIndicator(transform.position, hit, _indicator);
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                _command.Execute();
            }
            return;
        }       
        _indicator.SetActive(false);
    }

    private void Rotate(Vector3 mousePosition)
    {
        var direction = mousePosition - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = rotation;
    }
}
