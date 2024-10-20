using System.Collections;
using UnityEngine;

public class controllforphone : MonoBehaviour
{
    public float rotationSpeed = 100f;

    private Vector2 touchStartPos;
    private bool isTouching = false;
    public GameObject animhint;
    private void Start()
    {
        StartCoroutine(hintanim());
    }
    private IEnumerator hintanim()
    {
        yield return new WaitForSeconds(1);
        animhint.SetActive(true);
    }
    private void Update()
    {
        if (Input.touchCount > 0)
        {
            animhint.SetActive(false);
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                touchStartPos = touch.position;
                isTouching = true;
            }
            else if (touch.phase == TouchPhase.Moved && isTouching)
            {
                Vector2 touchDelta = touch.position - touchStartPos;
                float rotateInput = touchDelta.x;

                transform.localRotation *= Quaternion.Euler(0f, 0f, -rotateInput * rotationSpeed * Time.deltaTime);
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                isTouching = false;
            }
        }
    }
}
