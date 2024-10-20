using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activedefend : MonoBehaviour
{
    public GameObject glass;
    public GameObject buttonactive;
    public GameObject buttondeactive;

    private void Update()
    {
        

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            glass.SetActive(true);
            buttondeactive.SetActive(true);
            buttonactive.SetActive(false);
}
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            buttondeactive.SetActive(false);
            buttonactive.SetActive(true);
            glass.SetActive(false);

        }
    }
}
