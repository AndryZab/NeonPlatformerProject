using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserbutton : MonoBehaviour
{
    public GameObject decorOn;
    public GameObject decorOFF;
    public GameObject laser;
    public GameObject buttonOn;
    public GameObject buttonOff;
    public GameObject particleeffect;
    private Audiomanager audiomanager;
    private void Awake()
    {
        audiomanager = FindAnyObjectByType<Audiomanager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            audiomanager.PlaySFX(audiomanager.buttonlaser);
            decorOn.SetActive(true);
            decorOFF.SetActive(false);
            laser.SetActive(false);
            buttonOn.SetActive(false);
            if (buttonOff != null)
            {
               buttonOff.SetActive(true);

            }
            if(particleeffect != null)
            {
                particleeffect.SetActive(false);
            }


        }
    }
}
