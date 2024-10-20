using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trampolinescript : MonoBehaviour
{
    private Animator anim;
    private bool playercolision = false;
    private Player player;
    public int forceAmount = 10;
    private bool endanim = true;
    private bool startanim = true;
    private Audiomanager audiomanager;
    private Collider2D colider;
    private bool notcollision = false;

    private void Awake()
    {
        audiomanager = FindAnyObjectByType<Audiomanager>();
        player = FindAnyObjectByType<Player>();
        anim = GetComponent<Animator>();
        colider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        
        if (colider.offset.x == colider.offset.x)
        {
            notcollision = true;
        }
        if (playercolision == false)
        {
            anim.enabled = false;
        }
        else if (playercolision == true)
        {
            anim.enabled = true;
        }
      
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Vector2 velocityDirection = new Vector2(0, 2);
            player.rb.velocity = velocityDirection * forceAmount;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && endanim == true)
        {
            
            ContactPoint2D contact = collision.contacts[0];
            Vector2 contactNormal = contact.normal;

            
            bool isTopOrBottomCollision = Mathf.Abs(contactNormal.y) > Mathf.Abs(contactNormal.x);

            if (isTopOrBottomCollision)
            {
                if (audiomanager.trampoline != null)
                {
                    audiomanager.PlaySFX(audiomanager.trampoline);
                }
                playercolision = true;
                anim.SetBool("allowreverse", false);
                if (startanim == true)
                {
                    endanim = false;
                    startanim = false;
                }
            }
        }
    }

    public void AnimationEnd()
    {
        anim.SetBool("allowreverse", true);
        startanim = true;
    }

    public void Endreverse()
    {
        endanim = true;
    }
}
