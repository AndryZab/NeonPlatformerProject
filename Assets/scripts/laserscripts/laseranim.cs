using System.Collections;
using UnityEngine;

public class LaserExtendRetract : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Transform startPoint;
    public Transform endPoint;
    public Collider2D childObject;
    public float extendDuration = 1.0f;
    public float retractDuration = 1.0f;
    public float delayBetweenCycles = 1.0f;
    public Material sharedMaterial;
    public float scrollSpeedX;

    private bool isExtending = false;
    private PlayerDeath death;
    private bool playerHit = false;
    private Vector2 initialColliderSize;

    private void Awake()
    {
        death = FindObjectOfType<PlayerDeath>();
    }

    void Start()
    {
        if (lineRenderer != null && startPoint != null && endPoint != null)
        {
            lineRenderer.SetPosition(1, startPoint.position);
            lineRenderer.SetPosition(0, startPoint.position);
            if (childObject != null)
            {
                childObject.offset = new Vector2(childObject.offset.x, 0);
                if (childObject is BoxCollider2D boxCollider)
                {
                    initialColliderSize = boxCollider.size;
                }
                else if (childObject is CapsuleCollider2D capsuleCollider)
                {
                    initialColliderSize = capsuleCollider.size;
                }
            }
            StartCoroutine(LaserCycle());
        }
    }

    private void Update()
    {
        ScrollTexture();
    }

    IEnumerator LaserCycle()
    {
        while (true)
        {
            yield return StartCoroutine(ExtendLaser());
            yield return new WaitForSeconds(delayBetweenCycles);
            yield return StartCoroutine(RetractLaser());
            yield return new WaitForSeconds(delayBetweenCycles);
        }
    }

    IEnumerator ExtendLaser()
    {
        isExtending = true;
        float elapsedTime = 0f;

        while (elapsedTime < extendDuration)
        {
            if (lineRenderer != null)
            {
                Vector3 newPosition = Vector3.Lerp(startPoint.position, endPoint.position, elapsedTime / extendDuration);
                lineRenderer.SetPosition(1, newPosition);
                if (childObject != null)
                {
                    Vector3 centerPosition = (startPoint.position + newPosition) / 2;
                    childObject.transform.position = new Vector3(centerPosition.x, centerPosition.y, childObject.transform.position.z);

                    float scale = Vector3.Distance(startPoint.position, newPosition) / Vector3.Distance(startPoint.position, endPoint.position);
                    UpdateCollider(childObject, scale);
                }
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        if (lineRenderer != null)
        {
            lineRenderer.SetPosition(1, endPoint.position);
            if (childObject != null)
            {
                Vector3 centerPosition = (startPoint.position + endPoint.position) / 2;
                childObject.transform.position = new Vector3(centerPosition.x, centerPosition.y, childObject.transform.position.z);

                UpdateCollider(childObject, 1.0f);
            }
        }

        isExtending = false;
    }

    IEnumerator RetractLaser()
    {
        float elapsedTime = 0f;

        while (elapsedTime < retractDuration)
        {
            if (lineRenderer != null)
            {
                Vector3 newPosition = Vector3.Lerp(endPoint.position, startPoint.position, elapsedTime / retractDuration);
                lineRenderer.SetPosition(1, newPosition);
                if (childObject != null)
                {
                    Vector3 centerPosition = (startPoint.position + newPosition) / 2;
                    childObject.transform.position = new Vector3(centerPosition.x, centerPosition.y, childObject.transform.position.z);

                    float scale = Vector3.Distance(startPoint.position, newPosition) / Vector3.Distance(startPoint.position, endPoint.position);
                    UpdateCollider(childObject, scale);
                }
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        if (lineRenderer != null)
        {
            lineRenderer.SetPosition(1, startPoint.position);
            if (childObject != null)
            {
                childObject.transform.position = new Vector3(startPoint.position.x, startPoint.position.y, childObject.transform.position.z);

                UpdateCollider(childObject, 0);
            }
        }
    }

    void ScrollTexture()
    {
        if (sharedMaterial != null)
        {
            float currentY = sharedMaterial.mainTextureOffset.y;
            float offsetX = sharedMaterial.mainTextureOffset.x;
            offsetX -= Time.deltaTime * scrollSpeedX;

            if (offsetX <= -100f)
            {
                offsetX = 0f;
            }

            sharedMaterial.mainTextureOffset = new Vector2(offsetX, currentY);
        }
    }

    private void UpdateCollider(Collider2D collider, float scale)
    {
        if (collider is BoxCollider2D boxCollider)
        {
            boxCollider.size = new Vector2(initialColliderSize.x, initialColliderSize.y * scale);
        }
        else if (collider is CapsuleCollider2D capsuleCollider)
        {
            capsuleCollider.size = new Vector2(initialColliderSize.x, initialColliderSize.y * scale);
        }
    }
}
