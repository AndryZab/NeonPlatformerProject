using UnityEngine;

public class Background : MonoBehaviour
{
    public Transform target;
    public float minHorizontalParallaxSpeed = 1f;
    public float maxHorizontalParallaxSpeed = 2.6f;
    public float minVerticalParallaxSpeed = 1f;
    public float maxVerticalParallaxSpeed = 2.6f;
    public float smoothTime = 0.2f;
    private float transitionSpeed = 3f;

    public Rigidbody2D playerbody;

    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        
        float horizontalParallaxSpeed = Mathf.Lerp(minHorizontalParallaxSpeed, maxHorizontalParallaxSpeed, distance / 10f);
        float verticalParallaxSpeed = Mathf.Lerp(minVerticalParallaxSpeed, maxVerticalParallaxSpeed, distance / 10f);

        
        float horizontalParallax = (target.position.x - transform.position.x) * horizontalParallaxSpeed;
        float verticalParallax = (target.position.y - transform.position.y) * verticalParallaxSpeed;

        
        Vector3 targetPosition = new Vector3(transform.position.x + horizontalParallax, transform.position.y + verticalParallax, transform.position.z);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        

    }
    private void FixedUpdate()
    {
        float currentYVelocity = playerbody.velocity.y;

        if (playerbody != null && Mathf.Abs(currentYVelocity) > 20f)
        {
            maxVerticalParallaxSpeed = Mathf.Lerp(maxVerticalParallaxSpeed, 5f, transitionSpeed * Time.deltaTime);
        }
        else
        {
            maxVerticalParallaxSpeed = 1;
        }
    }
}
