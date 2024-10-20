using UnityEngine;

public class movetexture : MonoBehaviour
{
    private float previousXPosition;
    public int scrollspeedX;
    public GameObject saw;

    private void Start()
    {
        previousXPosition = saw.transform.position.x;
    }

    private void Update()
    {
        Renderer renderer = GetComponent<Renderer>();
        Material material = renderer.material;
        float currentXPosition = saw.transform.position.x;
        float currentX = material.mainTextureOffset.x;

        if (currentXPosition > previousXPosition)
        {
            currentX += Time.deltaTime * scrollspeedX;
        }
        else if (currentXPosition < previousXPosition)
        {
            currentX -= Time.deltaTime * scrollspeedX;
        }

        material.mainTextureOffset = new Vector2(currentX, 0);
        previousXPosition = currentXPosition;
    }
}
