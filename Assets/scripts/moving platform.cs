using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI;

public class movingplatform : MonoBehaviour
{
    [SerializeField] private GameObject sawObject;
    [SerializeField] private GameObject[] waypoints;
    [SerializeField] private float sawObjectsSpeed = 5.5f;
    [SerializeField] private float speed = 4.3f;
    private int currentWaypointIndex = 0;
    private void Update()
    {
        if (Vector2.Distance(waypoints[currentWaypointIndex].transform.position, transform.position) < .1f)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, Time.deltaTime * speed);
        if(sawObject != null)
        {
           sawObject.transform.position = Vector2.MoveTowards(sawObject.transform.position, waypoints[currentWaypointIndex].transform.position, Time.deltaTime * sawObjectsSpeed);
        }



    }

  

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
        if (collision.gameObject.name == "Player")
        {
            if (playerRb != null)
            {
                playerRb.interpolation = RigidbodyInterpolation2D.None;
                collision.gameObject.transform.SetParent(transform);
                
            }
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                playerRb.interpolation = RigidbodyInterpolation2D.Interpolate;
                collision.gameObject.transform.SetParent(null);

            }
        }
        
    }
  


}
