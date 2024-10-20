using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserfollow : MonoBehaviour
{
    public LayerMask raycastLayerMask;
    private PlayerDeath PlayerDeathScript;
    [SerializeField] private float DefDistanceRay = 100;
    public Transform laserfirepoint;
    private LineRenderer m_lineRenderer;
    Transform m_transform;
    private bool playerHit = false;
    public ParticleSystem sparticleSystem;
    [SerializeField] private float scrollSpeedX = 0.5f;
    private float cordY;
    private float offsetX;
    public Material sharedMaterial;
    private Player player;
    
    private void Awake()
    {

        m_lineRenderer = GetComponent<LineRenderer>();
        player = FindAnyObjectByType<Player>();
        PlayerDeathScript = FindAnyObjectByType<PlayerDeath>();
    }
 
    void LateUpdate()
    {
       
        RaycastHit2D hit = Physics2D.Raycast(laserfirepoint.position, transform.right, DefDistanceRay, raycastLayerMask);
        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Player") && !playerHit)
            {
                PlayerDeathScript.Die();
                playerHit = true;
                if (PlayerDeathScript.deathCanvas)
                {
                    playerHit = false;
                }
            }

            Vector2 endPos = hit.point != null ? hit.point : (Vector2)laserfirepoint.transform.right * DefDistanceRay;
            Draw2DRay(laserfirepoint.position, endPos);
        }
        ScrollTexture();
    }

    void Draw2DRay(Vector2 StartPos, Vector2 EndPos)
    {
        m_lineRenderer.SetPosition(0, StartPos);
        m_lineRenderer.SetPosition(1, EndPos);

        if (sparticleSystem != null)
        {
            sparticleSystem.transform.position = EndPos;
        }
    }

    void ScrollTexture()
    {
        float currentY = sharedMaterial.mainTextureOffset.y;

        if (offsetX <= 0)
        {
            offsetX -= Time.deltaTime * scrollSpeedX;

            if (offsetX <= -100f)
            {
                offsetX = 0f;
            }
        }

        sharedMaterial.mainTextureOffset = new Vector2(offsetX, currentY);
    }

}
