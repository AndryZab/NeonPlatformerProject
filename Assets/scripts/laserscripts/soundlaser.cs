using UnityEngine;
using System.Collections.Generic;

public class SoundLaser : MonoBehaviour
{
    public AudioSource sharedAudioSource;
    public Transform playerTransform;
    public List<LineRendererDistance> lineRenderersToCalculateDistance;
    public List<ObjectDistance> objectsToCalculateDistance;
    public float defaultMaxVolumeDistance = 7f;
    [SerializeField] public AudioSource laser;
    private Player player;

    [System.Serializable]
    public class LineRendererDistance
    {
        public LineRenderer lineRenderer;
        public float maxVolumeDistance = 7f;
    }

    [System.Serializable]
    public class ObjectDistance
    {
        public Transform objTransform;
        public float maxVolumeDistance = 7f;
    }

    private void Awake()
    {
        player = GetComponent<Player>();
     
    }

    void Update()
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

                            float distance = DistanceToSegment(playerTransform.position, startPoint, endPoint);

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

            // Обчислення для об'єктів
            if (objectsToCalculateDistance != null)
            {
                foreach (ObjectDistance objDist in objectsToCalculateDistance)
                {
                    if (objDist != null && objDist.objTransform != null && objDist.objTransform.gameObject.activeInHierarchy)
                    {
                        float distance = Vector3.Distance(playerTransform.position, objDist.objTransform.position);
                        float maxVolumeDistance = objDist.maxVolumeDistance > 0 ? objDist.maxVolumeDistance : defaultMaxVolumeDistance;
                        float volume = 1f - Mathf.Clamp01(distance / maxVolumeDistance);

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


    private float DistanceToSegment(Vector3 point, Vector3 segmentStart, Vector3 segmentEnd)
    {
        Vector3 segmentVector = segmentEnd - segmentStart;
        Vector3 pointVector = point - segmentStart;

        float segmentLengthSquared = segmentVector.sqrMagnitude;
        if (segmentLengthSquared == 0) return Vector3.Distance(point, segmentStart);

        float t = Mathf.Clamp01(Vector3.Dot(pointVector, segmentVector) / segmentLengthSquared);
        Vector3 projection = segmentStart + t * segmentVector;
        return Vector3.Distance(point, projection);
    }
}
