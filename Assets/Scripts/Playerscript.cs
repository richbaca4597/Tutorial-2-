using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Playerscript : MonoBehaviour
{

    private Rigidbody2D rd2d;

    public Rigidbody2D rb; 

    public float speed;
    public Text LivesText;
    public Text score;
    public Text WinText;

    public Text LoseText;
    private int Lives;
    private int scoreValue = 0;
    public float buttonTime = 0.3f;
    public float jumpAmount = 20;
    float jumpTime;
    bool jumping;
    bool isGrounded;
    private bool isOnGround;
    public Transform groundcheck;
    public float checkRadius;
    public LayerMask allGround;
    private bool facingRight = true;

    public AudioClip musicClipOne;

    public AudioClip musicClipTwo;

    public AudioSource musicSource;

    bool gameOver;


    Animator anim;





    // Start is called before the first frame update

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            jumping = true;
            jumpTime = 0;
            anim.SetInteger("State", 2);
            
        }

        if(jumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpAmount);
            jumpTime += Time.deltaTime;
        }

        if(Input.GetKeyUp(KeyCode.W) | jumpTime > buttonTime)
        {
            jumping = false; 
            anim.SetInteger("State", 0);
        }
          if(Input.GetKeyDown(KeyCode.D))
        {
            anim.SetInteger("State", 1);
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            anim.SetInteger("State", 0);
        }

                if(Input.GetKeyDown(KeyCode.A))
        {
            anim.SetInteger("State", 1);
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            anim.SetInteger("State", 0);
        }
    }
    void update()
    {
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpAmount) , ForceMode2D.Impulse);
               isGrounded = false;
        }


        
    }


    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = scoreValue.ToString();
        isGrounded = true;
        rb = GetComponent<Rigidbody2D>();
        Lives = 3;
        WinText.text = "";
        LivesText.text = "";
        LivesText.text = Lives.ToString();
        LoseText.text = "";
        musicSource.clip = musicClipOne;
        musicSource.Play();
        musicSource.loop = true;
        gameOver = false; 
        anim = GetComponent<Animator>();
        
        
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");

        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
        isOnGround = Physics2D.OverlapCircle(groundcheck.position, checkRadius, allGround);
        
        if (facingRight == false && hozMovement > 0)
        {
            Flip();
        }

        else if (facingRight == true && hozMovement < 0)
        {
            Flip();
        }
        if (Lives == 0)
        {
            Destroy(rb);
            Destroy(anim);
            musicSource.Stop();
        }

    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }

   private void OnCollisionEnter2D(Collision2D collision)
   {
       if(collision.collider.tag == "Coin")
       {
           Destroy(collision.collider.gameObject);
           scoreValue += 1;
           score.text = scoreValue.ToString();
           if (scoreValue >= 8)
           {
               WinText.text = "You Win! Game by Richard Bacallao";
           }
                  if (scoreValue == 4)
       {
          transform.position = (new Vector3(54.11f, 0.08f, 0.0f));
        
       } 



       if (scoreValue >= 8)
       {
           musicSource.clip = musicClipOne;
        musicSource.Stop();

        musicSource.clip = musicClipTwo;
        musicSource.Play(); 
        gameOver = true; 
        musicSource.loop = false;
        
       }

       }
       if(collision.collider.tag == "Enemy")
       {
           Destroy(collision.collider.gameObject);
           Lives -= 1;
           LivesText.text = Lives.ToString();
           if (Lives <= 0)
           {
               LoseText.text = "You Lose!";
               WinText.text = "";
               
           }
       }


   }



   private void OnCollisionStay2D(Collision2D collision)
   {
       if(collision.collider.tag == "Ground" && isOnGround)
       {
           if(Input.GetKey(KeyCode.W))
           {
               rd2d.AddForce(new Vector2(0, jumpAmount), ForceMode2D.Impulse);
           }
       }
              if (gameObject.tag == "Ground" && isGrounded == false)
       {
           isGrounded = true;
       }
       

   }
   





}
