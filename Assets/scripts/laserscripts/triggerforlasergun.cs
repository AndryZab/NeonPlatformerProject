using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerForLaserGun : MonoBehaviour
{
    public AudioSource laserGunSource;
    public bool stopsound;

  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (laserGunSource != null)
            {
                stopsound = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            stopsound = false;
        }
    }
}
