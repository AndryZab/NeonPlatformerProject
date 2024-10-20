using System.Collections;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private float fadeDuration = 10f;
    private float disableDuration = 8.5f;
    public Material materialDisolveMove;
    public laserhit allowdisolve;

    private Coroutine fadeCoroutine = null;
    private Coroutine disableCoroutine = null;
    private float fadeTimer = 0f;
    private float disableTimer = 0f;
    private bool isPaused = false;
    public GameObject objectWithMaterial;

    private Material materialForLine;

    private void Awake()
    {
        Renderer renderer = objectWithMaterial.GetComponent<Renderer>();

        
        if (renderer != null)
        {
            
            materialForLine = renderer.material;
        }
       
    }
    private void Update()
    {
        if (allowdisolve.OnDisolve)
        {
            if (isPaused)
            {
                isPaused = false;
                if (fadeCoroutine == null)
                {
                    fadeCoroutine = StartCoroutine(DisappearEffect());
                }
                if (disableCoroutine == null)
                {
                    disableCoroutine = StartCoroutine(DisableAfterTime());
                }
            }
        }
        else
        {
            if (!isPaused)
            {
                isPaused = true;
                if (fadeCoroutine != null)
                {
                    StopCoroutine(fadeCoroutine);
                    fadeCoroutine = null;
                }
                if (disableCoroutine != null)
                {
                    StopCoroutine(disableCoroutine);
                    disableCoroutine = null;
                }
            }
        }
    }

    public IEnumerator DisappearEffect()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Material material = spriteRenderer.material;

        while (fadeTimer < fadeDuration)
        {
            if (isPaused)
            {
                yield return null;
                continue;
            }

            fadeTimer += Time.deltaTime;
            float alpha = Mathf.Lerp(1.7f, 0.0f, fadeTimer / fadeDuration);
            material.SetFloat("Vector1_E974001A", alpha);
            materialForLine.SetFloat("Vector1_E974001A", alpha);
            yield return null;
        }

        fadeCoroutine = null;
    }

    public IEnumerator DisableAfterTime()
    {
        while (disableTimer < disableDuration)
        {
            if (isPaused)
            {
                yield return null;
                continue;
            }

            disableTimer += Time.deltaTime;
            yield return null;
        }

        gameObject.SetActive(false);
        disableCoroutine = null;
    }
}
