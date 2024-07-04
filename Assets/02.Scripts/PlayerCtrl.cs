using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerCtrl : MonoBehaviour
{
    Animator anim;
    public GameObject body;

    [HideInInspector]
    public bool dirRight = true;

    [HideInInspector]
    public bool jump = false;
    public float jumpForce = 185f;
    public AudioClip jumpClip;

    public AudioClip deadClip;

    private bool grounded = false;
    private Transform groundCheck;

    public float moveForce = 50f;
    public float maxSpeed = 1f;

    public AudioSource bgm;

    Rigidbody2D rb;

    private void Awake()
    {
        bgm=GetComponent<AudioSource>();

        groundCheck = transform.Find("groundCheck");
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        
    }
    private void Start()
    {
        body.gameObject.SetActive(false);
        bgm.playOnAwake = true;
        bgm.volume = 0.3f;
        
    }
    void Update()
    {
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        if(Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            jump = true;
        }
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        anim.SetFloat("Speed", Mathf.Abs(h));

        if(h * rb.velocity.x < maxSpeed)
        {
            rb.AddForce(Vector2.right * h * moveForce);
        }

        if(Mathf.Abs(rb.velocity.x) > maxSpeed)
        {
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
        }

        if(h > 0 && !dirRight)
        {
            Flip();
        }
        else if(h <0 && dirRight)
        {
            Flip();
        }

        if (jump)  
        {
            anim.SetTrigger("Jump");

            AudioSource.PlayClipAtPoint(jumpClip, transform.position);
            rb.AddForce(new Vector2(0.0f, jumpForce));

            
            jump = false;
        }
    }

    void Flip()
    {
        dirRight = !dirRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Dead")
        {

            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<FollowCamera>().enabled = false;

            AudioSource.PlayClipAtPoint(deadClip, transform.position);
            
            bgm.Stop();
            anim.SetTrigger("Dead");
            Destroy(collision.gameObject);
        }

    }


}
