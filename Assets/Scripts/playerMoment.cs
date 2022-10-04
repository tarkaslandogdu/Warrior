using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class playerMoment : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] float maxSpeed = 4f;
    [SerializeField] float jumpForce = 9f;
    [Header("Effects")]
    [SerializeField] GameObject m_JumpDust;
    [SerializeField] GameObject m_LandingDust;
    [Header("Ground")]
    [SerializeField] private LayerMask jumpingGround;

    private Animator anim;
    private Rigidbody2D rb2D;
    private BoxCollider2D coli;
    private AudioSource aSource;
    private audioManager aManager;
    private bool isGrounded = false;
    private bool isMoving = false;
    private bool jump;
    private int facingDirection = 1;
    private float horizontal = 0f;


    void Start()
    {
        anim = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        coli = GetComponent<BoxCollider2D>();
        aSource = GetComponent<AudioSource>();
        aManager = audioManager.instance;
    }

    void Update()
    {
        if (!isGrounded && IsGrounded())
        {
            isGrounded = true;
            anim.SetBool("Grounded", isGrounded);
        }

        if (isGrounded && !IsGrounded())
        {
            isGrounded = false;
            anim.SetBool("Grounded", isGrounded);
        }

        //input and movemet

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            jump = true;
        }

        float inputRaw = Input.GetAxisRaw("Horizontal");
        horizontal = Input.GetAxisRaw("Horizontal");
        rb2D.velocity = new Vector2(horizontal * maxSpeed * Time.deltaTime, rb2D.velocity.y);

        if (jump)
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x, jumpForce * Time.deltaTime);
            jump = false;
        }

        if (inputRaw > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            facingDirection = 1;
        }

        else if (inputRaw < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            facingDirection = -1;
        }

        anim.SetFloat("AirSpeedY", rb2D.velocity.y);

        UpdateAnimation();
    }

    private void UpdateAnimation()
    {

        if (horizontal > 0f)
        {
            isMoving = true;
        }
        else if (horizontal < 0f)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }


    //Animations
     //Jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            anim.SetTrigger("Jump");
            isGrounded = false;
            anim.SetBool("Grounded", isGrounded);
            rb2D.velocity = new Vector2(rb2D.velocity.x, jumpForce);
        }

        //Run
        else if (isMoving && isGrounded)
            anim.SetInteger("AnimState", 1);

        //Idle
        else
            anim.SetInteger("AnimState", 0);
    }


    void SpawnDustEffect(GameObject dust, float dustXOffset = 0)
    {
        if (dust != null)
        {
            Vector3 dustSpawnPosition = transform.position + new Vector3(dustXOffset * facingDirection, -1f, 0.0f);
            GameObject newDust = Instantiate(dust, dustSpawnPosition, Quaternion.identity) as GameObject;

            newDust.transform.localScale = newDust.transform.localScale.x * new Vector3(facingDirection, 1, 1);
        }
    }

    void AE_footstep()
    {
        aManager.PlaySound("Footstep");
    }

    void AE_Jump()
    {
        aManager.PlaySound("Jump");
        SpawnDustEffect(m_JumpDust);
    }

    void AE_Landing()
    {
        aManager.PlaySound("Landing");
        SpawnDustEffect(m_LandingDust);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coli.bounds.center, coli.bounds.size, 0f, Vector2.down, .2f, jumpingGround);
    }
}