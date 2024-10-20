using System.Collections;
using UnityEngine;

public class laserroatetandmove : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    [SerializeField] private float speed = 4.3f;
    private int currentWaypointIndex = 0;
    private bool moveForward = true;
    private Quaternion targetRotation;
    private float rotationSpeed = 90; 

    void Update()
    {
        
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, Time.deltaTime * speed);

        
        if (Vector2.Distance(transform.position, waypoints[currentWaypointIndex].transform.position) < .005f)
        {
            
            if (moveForward)
            {
                currentWaypointIndex++;
                if (currentWaypointIndex >= waypoints.Length)
                {
                    currentWaypointIndex = waypoints.Length - 2;
                    moveForward = false; 
                }
            }
            else
            {
                currentWaypointIndex--;
                if (currentWaypointIndex < 0)
                {
                    currentWaypointIndex = 1;
                    moveForward = true; 
                }
            }


            
        }

    }
    private void FixedUpdate()
    {
        if (currentWaypointIndex == 13 && moveForward == true)
        {
            targetRotation = Quaternion.Euler(0, 0, 90);
            StartCoroutine(RotateObject(targetRotation));
        }
        else if (currentWaypointIndex == 12 && moveForward == true)
        {
            targetRotation = Quaternion.Euler(0, 0, 0);
            StartCoroutine(RotateObject(targetRotation));
        }
        else if (currentWaypointIndex == 11 && moveForward == true)
        {
            targetRotation = Quaternion.Euler(0, 0, -90);
            StartCoroutine(RotateObject(targetRotation));
        }
        else if (currentWaypointIndex == 10 && moveForward == true)
        {
            targetRotation = Quaternion.Euler(0, 0, 0);
            StartCoroutine(RotateObject(targetRotation));
        }
        else if (currentWaypointIndex == 9 && moveForward == true)
        {
            targetRotation = Quaternion.Euler(0, 0, 90);
            StartCoroutine(RotateObject(targetRotation));
        }
        else if (currentWaypointIndex == 8 && moveForward == true)
        {
            targetRotation = Quaternion.Euler(0, 0, 0);
            StartCoroutine(RotateObject(targetRotation));
        }
        else if (currentWaypointIndex == 7 && moveForward == true)
        {
            targetRotation = Quaternion.Euler(0, 0, -90);
            StartCoroutine(RotateObject(targetRotation));
        }
        else if (currentWaypointIndex == 6 && moveForward == true)
        {
            targetRotation = Quaternion.Euler(0, 0, 0);
            StartCoroutine(RotateObject(targetRotation));
        }
        else if (currentWaypointIndex == 5 && moveForward == true)
        {
            targetRotation = Quaternion.Euler(0, 0, 90);
            StartCoroutine(RotateObject(targetRotation));
        }
        else if (currentWaypointIndex == 4 && moveForward == true)
        {
            targetRotation = Quaternion.Euler(0, 0, 0);
            StartCoroutine(RotateObject(targetRotation));
        }
        else if (currentWaypointIndex == 3 && moveForward == true)
        {
            targetRotation = Quaternion.Euler(0, 0, 90);
            StartCoroutine(RotateObject(targetRotation));
        }
        else if (currentWaypointIndex == 2 && moveForward == true)
        {
            targetRotation = Quaternion.Euler(0, 0, 180);
            StartCoroutine(RotateObject(targetRotation));
        }
        else if (currentWaypointIndex == 1 && moveForward == true)
        {
            targetRotation = Quaternion.Euler(0, 0, 90);
            StartCoroutine(RotateObject(targetRotation));
        }


        if (currentWaypointIndex == 12 && moveForward == false)
        {
            targetRotation = Quaternion.Euler(0, 0, 90);
            StartCoroutine(RotateObject(targetRotation));
        }
        else if (currentWaypointIndex == 11 && moveForward == false)
        {
            targetRotation = Quaternion.Euler(0, 0, 0);
            StartCoroutine(RotateObject(targetRotation));
        }
        else if (currentWaypointIndex == 10 && moveForward == false)
        {
            targetRotation = Quaternion.Euler(0, 0, -90);
            StartCoroutine(RotateObject(targetRotation));
        }
        else if (currentWaypointIndex == 9 && moveForward == false)
        {
            targetRotation = Quaternion.Euler(0, 0, 0);
            StartCoroutine(RotateObject(targetRotation));
        }
        else if (currentWaypointIndex == 8 && moveForward == false)
        {
            targetRotation = Quaternion.Euler(0, 0, 90);
            StartCoroutine(RotateObject(targetRotation));
        }
        else if (currentWaypointIndex == 7 && moveForward == false)
        {
            targetRotation = Quaternion.Euler(0, 0, 0);
            StartCoroutine(RotateObject(targetRotation));
        }
        else if (currentWaypointIndex == 6 && moveForward == false)
        {
            targetRotation = Quaternion.Euler(0, 0, -90);
            StartCoroutine(RotateObject(targetRotation));
        }
        else if (currentWaypointIndex == 5 && moveForward == false)
        {
            targetRotation = Quaternion.Euler(0, 0, 0);
            StartCoroutine(RotateObject(targetRotation));
        }
        else if (currentWaypointIndex == 4 && moveForward == false)
        {
            targetRotation = Quaternion.Euler(0, 0, 90);
            StartCoroutine(RotateObject(targetRotation));
        }
        else if (currentWaypointIndex == 3 && moveForward == false)
        {
            targetRotation = Quaternion.Euler(0, 0, 0);
            StartCoroutine(RotateObject(targetRotation));
        }
        else if (currentWaypointIndex == 2 && moveForward == false)
        {
            targetRotation = Quaternion.Euler(0, 0, 90);
            StartCoroutine(RotateObject(targetRotation));
        }
        else if (currentWaypointIndex == 1 && moveForward == false)
        {
            targetRotation = Quaternion.Euler(0, 0, 180);
            StartCoroutine(RotateObject(targetRotation));
        }
        else if (currentWaypointIndex == 0 && moveForward == false)
        {
            targetRotation = Quaternion.Euler(0, 0, 90);
            StartCoroutine(RotateObject(targetRotation));
        }
    }
    IEnumerator RotateObject(Quaternion targetRotation)
    {
            while (transform.rotation != targetRotation)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                yield return null;
            }
    }
}
