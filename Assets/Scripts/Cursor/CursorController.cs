using UnityEngine;

public class CursorController : MonoBehaviour
{
    [SerializeField] private Texture2D deafultIcon;
    [SerializeField] private Texture2D attackIcon;
    [SerializeField] private Camera mainCamera;

    void Update()
    {
        if(IsOverEnemy())
        {
            Cursor.SetCursor(attackIcon, Vector2.zero, CursorMode.Auto);
            return;
        }
        Cursor.SetCursor(deafultIcon, Vector2.zero, CursorMode.Auto);
    }

    private bool IsOverEnemy()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        bool didHit = RaycastHitUtil.ExludePlayerLayer(ray, out RaycastHit hit);
        if(!didHit) return false;

        return hit.transform.TryGetComponent(out IDamagable damagable);
    }
}
