using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lasercolliderforfollow : MonoBehaviour
{
    public scriptforrotatefollowtarget rotatelaser;
    private PlayerDeath death;
    private VideoController videobutton;

    private void Awake()
    {
        videobutton = FindAnyObjectByType<VideoController>();
        death = FindAnyObjectByType<PlayerDeath>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (videobutton != null && videobutton.allowrotate == false)
            {
                rotatelaser.allowforrotate = true;

            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            
             rotatelaser.allowforrotate = false;

            
        }
    }
}
