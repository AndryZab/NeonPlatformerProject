using System.Collections;
using UnityEngine;

public class ObjectVisibilityController : MonoBehaviour
{
    public float fadeSpeed = 0.5f;
    private Color targetColor;
    private SpriteRenderer spriteRenderer;
    private bool isPlayerInside = false;
    public Rigidbody2D rb;
    public Player playerslide;
    private bool isTriggerActive = false;
    public int jumpforce = 0;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        targetColor = spriteRenderer.color; // Початковий колір спрайту
    }

    private void Update()
    {
        if (rb != null && isTriggerActive == true)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpforce);
        }

      

        Color currentColor = spriteRenderer.color;
        Color newColor = Color.Lerp(currentColor, targetColor, fadeSpeed * Time.deltaTime);
        spriteRenderer.color = newColor;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isTriggerActive = true;
            isPlayerInside = true;
            StartCoroutine(StartInvisibilityTimer(0.05f));
            playerslide.slide = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (playerslide.buttonpressed == true)
            {
                playerslide.slide = true;
            }
            isTriggerActive = false;
            isPlayerInside = false;
            StartCoroutine(StartVisibilityTimer(0.1f));
        }
    }

    private IEnumerator StartInvisibilityTimer(float duration)
    {
        yield return new WaitForSeconds(duration);
        targetColor = Color.black; // Повернення до початкового колору після закінчення таймера
    }

    private IEnumerator StartVisibilityTimer(float duration)
    {
        yield return new WaitForSeconds(duration);
        targetColor = Color.white; // Затемнення після закінчення таймера
    }
}
