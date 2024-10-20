using UnityEngine;
using System.Collections;

public class laserhit : MonoBehaviour
{
    public LayerMask raycastLayerMask;
    private PlayerDeath PlayerDeathScript;
    [SerializeField] private float DefDistanceRay = 100;
    public Transform laserfirepoint;
    private LineRenderer m_lineRenderer;
    Transform m_transform;
    private bool playerHit = false;
    public GameObject laserbutonON;
    public GameObject laserbutonOFF;
    public ParticleSystem sparticleSystem;
    [SerializeField] private float scrollSpeedX = 0.5f;
    private float cordY;
    private float offsetX;
    public Material sharedMaterial;
    public bool OnDisolve = false;
    public AudioSource shieldsound;
   
  
    private void Awake()
    {
        if (shieldsound != null)
        {
            shieldsound.Pause();

        }

        m_transform = GetComponent<Transform>();
        m_lineRenderer = GetComponent<LineRenderer>();
        PlayerDeathScript = FindAnyObjectByType<PlayerDeath>();

    }

    void Start()
    {

        if (sparticleSystem != null)
        {
            sparticleSystem.Stop();
        }
    }

    private void LateUpdate()
    {
        ShootLaser();
        ScrollTexture();
    }

    void ShootLaser()
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
            if (hit.collider.CompareTag("buttonlasser"))
            {
                if (laserbutonON != null)
                {
                    laserbutonON.SetActive(false);
                }
                if (laserbutonOFF != null)
                {
                    laserbutonOFF.SetActive(true);
                }
            }
            if (hit.collider.CompareTag("Shield") && Time.timeScale != 0)
            {
                OnDisolve = true;
                if (sparticleSystem != null)
                {
                   sparticleSystem.Play();

                }
                if (shieldsound != null)
                {
                    shieldsound.UnPause();
                }
            }
            else
            {
               if (OnDisolve)
                {
                   OnDisolve = false;

                   if (sparticleSystem != null)
                   {
                     sparticleSystem.Stop();

                   }
                  
                }
            }

            if (shieldsound != null && !OnDisolve && !hit.collider.CompareTag("Shield"))
            {
                shieldsound.Pause();

            }
        }
        
        Draw2DRay(laserfirepoint.position, hit.point != null ? hit.point : (Vector2)laserfirepoint.transform.right * DefDistanceRay);
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
