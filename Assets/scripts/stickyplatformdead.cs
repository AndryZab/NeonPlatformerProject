using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyPlatformDead : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    [SerializeField] private float speed = 4.3f;
    private int currentWaypointIndex = 0;
    public GameObject spikeforstraight;
    public GameObject targetformove;
    private bool Moveplatform = false;
    private PlayerDeath playerdeath;

    private void Awake()
    {
        playerdeath = FindAnyObjectByType<PlayerDeath>();
    }


    private void Update()
    {
        if (playerdeath.checkforplatforDeadreset)
        {
            playerdeath.checkforplatforDeadreset = false;
            Moveplatform = false;

        }
        Debug.Log(Moveplatform);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Moveplatform == false)
        {
            spikeforstraight.SetActive(true);
            targetformove.SetActive(true);
            StartCoroutine(MovePlatform());
        }
    }

    private IEnumerator MovePlatform()
    {
        while (true)
        {
            while (Vector2.Distance(waypoints[currentWaypointIndex].transform.position, targetformove.transform.position) >= .1f)
            {
                targetformove.transform.position = Vector2.MoveTowards(targetformove.transform.position, waypoints[currentWaypointIndex].transform.position, Time.deltaTime * speed);
                yield return null;
            }

            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
                targetformove.transform.position = waypoints[0].transform.position;
                targetformove.SetActive(false);
                spikeforstraight.SetActive(false);
                if (Moveplatform != true)
                {
                   Moveplatform = true;

                }
                yield break;
            }
        }
    }
}
