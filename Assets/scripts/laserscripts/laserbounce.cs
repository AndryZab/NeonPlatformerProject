using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LaserScript2D : MonoBehaviour
{
    [Header("Settings")]
    public LayerMask layerMask;
    public float defaultLength = 50f;
    public int numOfReflections = 2;
    private LineRenderer _lineRenderer;
    private RaycastHit2D hit;
    private Ray2D ray;
    private Vector2 direction;

    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        ReflectLaser();
    }

    void ReflectLaser()
    {
        ray = new Ray2D(transform.position, transform.right); // Assuming the laser shoots to the right
        _lineRenderer.positionCount = 1;
        _lineRenderer.SetPosition(0, transform.position);
        float remainLength = defaultLength;
        for (int i = 0; i < numOfReflections; i++)
        {
            hit = Physics2D.Raycast(ray.origin, ray.direction, remainLength, layerMask);
            if (hit)
            {
                _lineRenderer.positionCount++;
                _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, hit.point);
                remainLength = Vector2.Distance(ray.origin, hit.point);
                ray = new Ray2D(hit.point, Vector2.Reflect(ray.direction, hit.normal));
            }
            else
            {
                _lineRenderer.positionCount++;
                _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, ray.origin + (ray.direction * remainLength));
            }
        }
    }
}
