using UnityEngine;

[CreateAssetMenu(fileName = "MoveIndicator", menuName = "Move/MoveIndicator")]
public class MoveIndicator : ScriptableObject
{
    [SerializeField] private GameObject moveIndicatorPrefab;
    public GameObject MoveIndicatorPrefab => moveIndicatorPrefab;
}
