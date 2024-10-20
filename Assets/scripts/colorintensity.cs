using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteHDRController: MonoBehaviour
{
    public float intensity = 1.0f;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    void Update()
    {
        spriteRenderer.color = originalColor * intensity;
    }
}
