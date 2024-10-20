using UnityEngine;

public class ObjectManipulation : MonoBehaviour
{
    public float moveSpeed = 5f; 
    public float scaleSpeed = 0.1f; 

    void Update()
    {
        
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            
            if (touch.phase == TouchPhase.Moved)
            {
                Vector3 touchDeltaPosition = touch.deltaPosition;
                Vector3 movement = new Vector3(touchDeltaPosition.x, touchDeltaPosition.y, 0f) * moveSpeed * Time.deltaTime;
                transform.Translate(movement);
            }

            
            if (touch.phase == TouchPhase.Began && touch.tapCount == 2) 
            {
                float scaleFactor = touch.deltaPosition.magnitude * scaleSpeed;
                transform.localScale += Vector3.one * scaleFactor;
            }
        }
    }
}
