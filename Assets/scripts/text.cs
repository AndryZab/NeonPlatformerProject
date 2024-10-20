using UnityEngine;
using TMPro;

public class FadeText : MonoBehaviour
{
    public TextMeshPro textmattery;
    public TextMeshPro textmattery1;
    public TextMeshPro textmattery2;
    public TextMeshPro textdoor;
    public float distanceThreshold = 5f;
    public float fadeSpeed = 0.1f;
    public float distanceThresholds = 2f;
    public float fadeSpeeds = 1f;

    private void Update()
    {
        if (textmattery != null)
            FadeTextByDistance(textmattery, distanceThreshold, fadeSpeed);

        if (textmattery1 != null)
            FadeTextByDistance(textmattery1, distanceThreshold, fadeSpeed);
        if (textmattery2 != null)
        {
            FadeTextByDistance(textmattery2, distanceThreshold, fadeSpeed);
        }

        if (textdoor != null)
            FadeTextByDistance(textdoor, distanceThreshold, fadeSpeed);
    }

    private void FadeTextByDistance(TextMeshPro text, float threshold, float speed)
    {
        float distance = Vector3.Distance(transform.position, text.transform.position);
        float targetAlpha = distance > threshold ? 0 : 1;
        float alpha = Mathf.Lerp(text.color.a, targetAlpha, speed * Time.deltaTime);
        text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
    }
}
