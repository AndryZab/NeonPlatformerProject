using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI;

public class bulletmovelaser : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    [SerializeField] private float speed = 4.3f;
    private int currentWaypointIndex = 0;
    private Player player;
    private bool allowed = false;
    private bool allowsound = false;
    public AudioSource audioSourceBullet;
    public AudioClip laserGun;
    public TriggerForLaserGun stopsound;

    private bool newbullet = false;
    private int count;

    private void Awake()
    {
        player = FindAnyObjectByType<Player>();
        audioSourceBullet.Stop();
        Invoke("applybool", 0.01f);
        audioSourceBullet.Play();

    }
    private void applybool()
    {
        allowsound = true;
    }
    
    private void Update()
    {
        if (Vector2.Distance(waypoints[currentWaypointIndex].transform.position, transform.position) < .1f)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length)
            {
                transform.position = waypoints[0].transform.position;
                currentWaypointIndex = 0;
            }
        }

        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, Time.deltaTime * speed);

       

        if (player.rb.bodyType == RigidbodyType2D.Static)
        {
            transform.position = waypoints[0].transform.position;
            allowed = true;
        }
        else if (player.rb.bodyType == RigidbodyType2D.Dynamic && allowed == true)
        {
            currentWaypointIndex = 0;
            allowed = false;
        }
        

        if (currentWaypointIndex == 0 && allowsound == true)
        {
            if (stopsound.stopsound == false) return;
            soundplay();
        }

    }
   
    private void soundplay()
    {
        if (audioSourceBullet != null && stopsound.stopsound == true)
        {
            
            audioSourceBullet.PlayOneShot(laserGun);
            
        }
    }
   


}
