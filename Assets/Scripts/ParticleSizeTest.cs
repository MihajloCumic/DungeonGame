using UnityEngine;

public class ParticleSizeTest : MonoBehaviour
{
    [SerializeField] private ParticleSystem ps;


    void Start()
    {
        var shape = ps.shape;
        shape.radius = 100f;
    }
}
