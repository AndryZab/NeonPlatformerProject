using System.Collections.Generic;
using UnityEngine;

public class laservolumefollow : MonoBehaviour
{
    public AudioSource sharedAudioSource;
    public List<LineRendererDistance> lineRenderersToCalculateDistance;
    public float defaultMaxVolumeDistance = 7f;
    [SerializeField] private AudioSource laser;
    private Player player;

    [System.Serializable]
    public class LineRendererDistance
    {
        public LineRenderer lineRenderer;
        public float maxVolumeDistance = 7f;
    }

    private void Awake()
    {
        player = FindAnyObjectByType<Player>(); 
    }

    private void Update()
    {
        float closestVolume = 0f;

        // Обчислення для LineRenderer
        if (lineRenderersToCalculateDistance != null)
        {
            foreach (LineRendererDistance lineRendererDist in lineRenderersToCalculateDistance)
            {
                if (lineRendererDist != null && lineRendererDist.lineRenderer != null && lineRendererDist.lineRenderer.gameObject.activeInHierarchy)
                {
                    float minDistance = float.MaxValue;

                    for (int i = 0; i < lineRendererDist.lineRenderer.positionCount - 1; i++)
                    {
                        Vector3 startPoint = lineRendererDist.lineRenderer.GetPosition(i);
                        Vector3 endPoint = lineRendererDist.lineRenderer.GetPosition(i + 1);

                        // Отримання відстані від персонажа до лінії
                        float distance = Vector3.Distance(player.transform.position, startPoint);
                        float distanceToEnd = Vector3.Distance(player.transform.position, endPoint);
                        distance = Mathf.Min(distance, distanceToEnd);

                        if (distance < minDistance)
                        {
                            minDistance = distance;
                        }
                    }

                    float maxVolumeDistance = lineRendererDist.maxVolumeDistance > 0 ? lineRendererDist.maxVolumeDistance : defaultMaxVolumeDistance;
                    float volume = 0.5f - Mathf.Clamp01(minDistance / maxVolumeDistance);

                    if (volume > closestVolume)
                    {
                        closestVolume = volume;
                    }
                }
            }
        }

        if (sharedAudioSource != null)
        {
            sharedAudioSource.volume = closestVolume;
        }
    }
}
