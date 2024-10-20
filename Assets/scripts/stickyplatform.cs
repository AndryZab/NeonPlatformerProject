using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Runtime.CompilerServices.RuntimeHelpers;

public class stickyplatform : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;

    public Player stickyplayer;
    [SerializeField] private float speed = 4.3f;
    private int currentWaypointIndex = 0;

    private void Update()
    {
        if (waypoints.Length != 0)
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
        }


    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
           
        
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
            stickyplayer.accesstycky = true;
            playerRb.interpolation = RigidbodyInterpolation2D.None;
            stickyplayer.wallJumpingPower = new Vector2(4f, 17f);
            collision.gameObject.transform.SetParent(transform);
        
        }
        
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            stickyplayer.accesstycky = false;
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
            playerRb.interpolation = RigidbodyInterpolation2D.Interpolate;
            collision.gameObject.transform.SetParent(null);
            stickyplayer.wallJumpingPower = new Vector2(15f, 17f);
            

        }
    }

}
