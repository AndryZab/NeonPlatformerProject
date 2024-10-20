using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class frictionforblock : MonoBehaviour
{
    public PhysicsMaterial2D friction;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Lick"))
        {
            CapsuleCollider2D rb = collision.GetComponent<CapsuleCollider2D>();
            if (rb != null)
            {
                rb.sharedMaterial = friction;
            }
        }
    }
}
