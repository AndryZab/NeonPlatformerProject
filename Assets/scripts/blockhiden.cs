using System.Collections;
using UnityEngine;

public class BlockHidden : MonoBehaviour
{
    public float fadeDuration = 1.0f;
    public float disolveAmountStart = 1.25f;
    private Player player;
    private float timer = 0f;

    private bool timerLeft = false;

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }
    private void Update()
    {
        if (timer >= fadeDuration && !timerLeft)
        {
            player.gameObject.transform.SetParent(null);
            Debug.Log("succes");
            timerLeft = true;
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            Time.timeScale = 0.1f;
        }
        else if(Input.GetKeyDown(KeyCode.P))
        {
            Time.timeScale = 1f;
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(DisappearEffect());
        }
    }

    IEnumerator DisappearEffect()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Material material = spriteRenderer.material;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(disolveAmountStart, 0.0f, timer / fadeDuration);
            material.SetFloat("Vector1_E974001A", alpha); 
            yield return null;
        }

        gameObject.SetActive(false);
    }
}
