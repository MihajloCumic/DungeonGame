using UnityEngine;

public class SpellCaster : MonoBehaviour
{

    private ICommand _command;
    [SerializeField] private FireSpell firespellSo;
    [SerializeField] private GameObject marker;
    private GameObject _indicator;
    private const float maxSize = 5f;

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
        if (Input.GetMouseButton(0))
        {
            
        }
        if (Input.GetKey(KeyCode.Q) && Input.GetMouseButton(0))
            {
                _indicator.transform.position = transform.position;

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit hit))
                {

                    var childsTransform = _indicator.GetComponentInChildren<Transform>();
                    Vector3 direction = hit.point - childsTransform.transform.position;
                    // var m = Instantiate(marker);
                    // m.transform.position = hit.point;
                    // return;
                    direction.y = 0;
                    Quaternion targetRotation = Quaternion.LookRotation(direction);
                    _indicator.transform.rotation = targetRotation;

                    //Scale
                    var tmpDistance = Vector3.Distance(_indicator.transform.position, hit.point);
                    var distance = tmpDistance > 5f ? tmpDistance : tmpDistance;

                    float scaleZ = distance / 10f;
                    var scaleX = childsTransform.localScale.x;
                    childsTransform.localScale = new Vector3(scaleX, 1f, scaleZ);
                }
                _indicator.SetActive(true);
                return;
            }
        if (Input.GetKey(KeyCode.Q) && Input.GetMouseButtonUp(0))
        {
            _command.Execute();
        }
        _indicator.SetActive(false);
    }
}
