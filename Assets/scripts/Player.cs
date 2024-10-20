using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using SimpleInputNamespace;
using UnityEditor;
using System.Collections.Generic;
using System.Collections;
using Cinemachine;
using Unity.VisualScripting;
public class Player : MonoBehaviour
{
    private bool canFlip = true;
    public bool isJumping = false;
    public bool slide = false;
    public float horizontal;
    public float speed = 10f;
    private float jumpingPower = 13f;
    private bool isFacingRight = true;


    public float wallSlidingSpeed = 2f;
    private bool isWallSliding;


    private bool isWallJumping = false;
    private float wallJumpingDirection;
    private float wallJumpingTime = 0.1f;
    private float wallJumpingCounter;
    private float wallJumpingDuration = 0.25f;
    public Vector2 wallJumpingPower = new Vector2(15f, 17f);
    [SerializeField] public Rigidbody2D rb;
    [SerializeField] public Transform groundCheck;
    [SerializeField] public LayerMask groundLayer;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;

    [SerializeField] public Transform allowjumpsoundcheck;
    [SerializeField] private Animator anim;

    Audiomanager audiomanager;
    private float jumpTime = 0.41f;
    private float jumpTimeCounter = 1;
    private bool bfs = true;
    private bool canJump = false;
    public bool styckiwall = false;
    public CinemachineVirtualCamera cameracinmachine;
    public float targetYDamping = 0.15f;
    public float dampingSpeed = 0.1f;
    public bool accesstycky = false;
    private CapsuleCollider2D col;
    public bool buttonpressed = false;



    private void EnableJump()
    {
        canJump = true;
    }


   

    private void Awake()
    {
        Invoke("EnableJump", 0.19f);
        audiomanager = GameObject.FindGameObjectWithTag("audio").GetComponent<Audiomanager>();
        col = GetComponent<CapsuleCollider2D>();
    }
    private void Update()
    {
        if (rb.bodyType != RigidbodyType2D.Static)
        {
          horizontal = SimpleInput.GetAxisRaw("Horizontal");
          Move();


      }

    }


    public void FixedUpdate()
    {
        
        float currentYVelocity = rb.velocity.y;
        float currentYDamping = cameracinmachine.GetCinemachineComponent<CinemachineFramingTransposer>().m_YDamping;

        

        if (Mathf.Abs(currentYVelocity) > 30f)
        {
            
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Sign(currentYVelocity) * 30f);
            

        }
        if (Mathf.Abs(currentYVelocity) > 20f)
        {
            if (cameracinmachine != null)
            {
               
                    currentYDamping = Mathf.Lerp(currentYDamping, targetYDamping, dampingSpeed);
                    cameracinmachine.GetCinemachineComponent<CinemachineFramingTransposer>().m_YDamping = currentYDamping;

                

            }
        }
        else
        {
            currentYDamping = Mathf.Lerp(currentYDamping, 1, dampingSpeed);
            cameracinmachine.GetCinemachineComponent<CinemachineFramingTransposer>().m_YDamping = currentYDamping;

        }

       

        if (IsGrounded())
        {
            bfs = true;
        }
        
        if (rb.bodyType != RigidbodyType2D.Static)
        {
           sticky();
           
           longjump();
           
           WallJump();
           
           if (isJumping)
           {
              Jump();
           }
  

           WallSlide();
        }
        



        if (slide && rb.bodyType != RigidbodyType2D.Static)
        {
            WallSlide();
        }


        if (!isWallJumping)
        {
            Flip();
        }


        SetAnimationParameters();
    }

    private void sticky()
    {
        if (IsTouchingLick() && styckiwall == true || accesstycky == true && slide == true)
        {
            rb.gravityScale = 0;


        }
        else
        {
            rb.gravityScale = 5;
        }
        if (accesstycky == true && slide == false)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -7, float.MaxValue));
        }



    }
    private void Move()
    {
        if (!isWallJumping && rb.bodyType == RigidbodyType2D.Dynamic)
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

            
            
        }
        
    }

    public void OnJumpButtonDown(BaseEventData eventData)
    {
        
       if (canJump && (IsGrounded() && !IsWalled() && !isWallSliding || !IsGrounded()))
       {
            isJumping = true;

       }
        
        
        
    }


    public void onSlide(BaseEventData data)
    {
        if (!IsGrounded() || isJumping)
        {
            slide = true;
            buttonpressed = true;
        }
        styckiwall = true;



    }

    public void onSlideUP(BaseEventData data)
    {
        buttonpressed = false;
        slide = false;
        rb.gravityScale = 5;
        styckiwall = false;
    }




    public void OnJumpButtonUp(BaseEventData eventData)
    {
        isJumping = false;
        bfs = false;
    }


    public void longjump()
    {
        if (jumpTimeCounter > 0 && jumpTime != 0f)
        {
            jumpTimeCounter -= Time.deltaTime;
            

        }
        else if (IsGrounded())
        {
            jumpTimeCounter = 0f;
        }
    }
    public void Jump()
    {

        if(allowjumpsound())
        {
            audiomanager.PlaySFX(audiomanager.jump);

        }
        if (IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            jumpTime = 0.3f;
            jumpTimeCounter = jumpTime;
        }

        if (jumpTimeCounter > 0 && jumpTime != 0f && bfs != false)
        {
            longjump();
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

      

    }






    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.10f, groundLayer);
    }

    public bool allowjumpsound()
    {
        return Physics2D.OverlapCircle(allowjumpsoundcheck.position, 0.10f, groundLayer);
    }
    private void OnDrawGizmos()
    {
        
        Gizmos.DrawWireSphere(groundCheck.position, 0.10f);
        Gizmos.DrawWireSphere(allowjumpsoundcheck.position, 0.10f);
        Gizmos.DrawWireSphere(wallCheck.position, 0.2f);
    }

    private bool IsWalled()
    {
        if (!IsGrounded())
        {
            float radius = 0.2f;

            bool onWall = Physics2D.OverlapCircle(wallCheck.position, radius, wallLayer);

            if (!onWall)
            {
                Vector2 extraCheck = wallCheck.position + new Vector3(0, radius + 0.1f);
                onWall = Physics2D.OverlapCircle(extraCheck, radius, wallLayer);
            }

            return onWall; 
        }

        return false; 
    }







    private void WallSlide()
    {
        if (IsWalled() && !IsGrounded() && slide == true)
        {
            if (IsTouchingLick() || accesstycky == true) 
            {
                isWallSliding = true;
                rb.velocity = new Vector2(0f, Mathf.Clamp(0f, 0f, 0f));

            }
            else
            {
                isWallSliding = true;
                rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
            }
        }
        else
        {
            isWallSliding = false;
        }
       

    }
   
    private bool IsTouchingLick()
    {
        
        Collider2D[] colliders = Physics2D.OverlapBoxAll(wallCheck.position, new Vector2(1, 1), 0f);
        foreach (Collider2D col in colliders)
        {
            if (col.CompareTag("Lick"))
            {
                return true;
            }
        }
        return false;
    }

    private void WallJump()
    {
        if (!isWallSliding || !IsWalled())
        {
            return;  
        }

        if (isWallSliding)
        {
            isWallJumping = false;
            wallJumpingDirection = -transform.localScale.x;
            wallJumpingCounter = wallJumpingTime;

            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }

        float horizontalInput = Input.GetAxis("Horizontal") + SimpleInput.GetAxisRaw("Horizontal");

        
        if (Mathf.Abs(horizontalInput) > 0 && wallJumpingCounter > 0f)
        {
            if ((horizontalInput > 0 && isFacingRight && IsWalled())
                || (horizontalInput < 0 && !isFacingRight && IsWalled()))
            {
                
                return;
            }
            audiomanager.PlaySFX(audiomanager.walltouch);
            isWallJumping = true;
            rb.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter = 0f;

            if (transform.localScale.x != wallJumpingDirection)
            {
                isFacingRight = !isFacingRight;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }

            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        }
    }





    private void StopWallJumping()
    {
        isWallJumping = false;
    }

    private void Flip()
    {
        if (canFlip && (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f))
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }


    public void DisableFlip()
    {
        canFlip = false;
    }

    public void EnableFlip()
    {
        canFlip = true;
    }


    private void SetAnimationParameters()
    {
        bool isMoving = IsMovingOnGround(); 

        anim.SetBool("isGrounded", IsGrounded());
        anim.SetBool("isMoving", isMoving);
        anim.SetBool("isWallSliding", isWallSliding);
    }


    private bool IsMovingOnGround()
    {
        return IsGrounded() && Mathf.Abs(rb.velocity.x) > 9.9;
    }
    
}