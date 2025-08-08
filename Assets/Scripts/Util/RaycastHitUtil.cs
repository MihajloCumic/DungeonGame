using UnityEngine;

public class RaycastHitUtil
{
    public static bool IncludePlayerLayer(Ray ray, out RaycastHit hit, float distance)
    {
        return Physics.Raycast(ray, out hit, distance);
    }
    public static bool IncludePlayerLayer(Ray ray, out RaycastHit hit)
    {
        return Physics.Raycast(ray, out hit);
    }
    public static bool ExludePlayerLayer(Ray ray, out RaycastHit hit, float distance)
    {
        int layer = LayerMask.NameToLayer("PlayerLayer");
        LayerMask exludePlayer = ~(1 << layer);
        return Physics.Raycast(ray, out hit, distance, exludePlayer);
    }
    public static bool ExludePlayerLayer(Ray ray, out RaycastHit hit)
    {
        int layer = LayerMask.NameToLayer("PlayerLayer");
        LayerMask exludePlayer = ~(1 << layer);
        return Physics.Raycast(ray, out hit, Mathf.Infinity,exludePlayer);
    }
}
