using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class scriptforrotatefollowtarget : MonoBehaviour
{
    private Player player;
    public bool allowforrotate = false;
    public float rotationSpeed = 3f;
    private RotationConstraint rotate;
    private PlayerDeath death;
    private VideoController videobutton;
    private void Awake()
    {
        videobutton = FindAnyObjectByType<VideoController>();
        death = FindAnyObjectByType<PlayerDeath>();
        rotate = GetComponent<RotationConstraint>();
        player = FindAnyObjectByType<Player>();
    }
    private void LateUpdate()
    {
        if (videobutton != null)
        {

           if (videobutton.allowrotate == true)
           {
               rotate.enabled = true;
           }
 
           else if (videobutton.allowrotate == false)
           {
               rotate.enabled = false;
           }
        } 

    


        if (player != null && allowforrotate == true)
        {

            Vector3 direction = player.transform.position - transform.position;


            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;


            Quaternion targetRotation = Quaternion.Euler(0, 0, angle);


            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
