using UnityEngine;
using Cinemachine;
using System.Collections;

public class CameraZoomController : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public float zoomedInSize = 5f;
    public float normalSize = 10f;
    public float zoomTime = 1f;

    public GameObject backgroundObject;
    public float zoomedInSizeX = 1f;
    public float zoomedInSizeY = 1f;
    public float normalSizeX = 2f;
    public float normalSizeY = 2f;
    public float backgroundZoomTime = 1f;

    private Coroutine zoomCoroutine;
    private Coroutine backgroundZoomCoroutine;
    private bool isChangingBackgroundSize = false;
   
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (zoomCoroutine != null)
                StopCoroutine(zoomCoroutine);

            zoomCoroutine = StartCoroutine(ChangeCameraSize(zoomedInSize));

            
            if (backgroundZoomCoroutine != null)
                StopCoroutine(backgroundZoomCoroutine);

            backgroundZoomCoroutine = StartCoroutine(ChangeBackgroundSize(new Vector3(zoomedInSizeX, zoomedInSizeY, 1f)));
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (zoomCoroutine != null)
                StopCoroutine(zoomCoroutine);

            zoomCoroutine = StartCoroutine(ChangeCameraSize(normalSize));

            if (backgroundZoomCoroutine != null)
                StopCoroutine(backgroundZoomCoroutine);

            backgroundZoomCoroutine = StartCoroutine(ChangeBackgroundSize(new Vector3(normalSizeX, normalSizeY, 1f)));
        }
    }

    private IEnumerator ChangeCameraSize(float targetSize)
    {
        float currentSize = virtualCamera.m_Lens.OrthographicSize;
        float timer = 0f;

        while (timer < zoomTime)
        {
            timer += Time.deltaTime;
            float newSize = Mathf.Lerp(currentSize, targetSize, timer / zoomTime);
            virtualCamera.m_Lens.OrthographicSize = newSize;
            yield return null;
        }

        zoomCoroutine = null;
    }

    private IEnumerator ChangeBackgroundSize(Vector3 targetSize)
    {
        isChangingBackgroundSize = true; 

        Vector3 currentSize = backgroundObject.transform.localScale;
        float timer = 0f;

        while (timer < backgroundZoomTime)
        {
            timer += Time.deltaTime;
            float t = timer / backgroundZoomTime;
            backgroundObject.transform.localScale = Vector3.Lerp(currentSize, targetSize, t);
            yield return null;
        }

        backgroundObject.transform.localScale = targetSize;
        backgroundZoomCoroutine = null;
    }
}
