using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{   
    // prive variabelen
    private Rigidbody2D rb;
    private Animator animatie;
    private Collider2D coll;

    // animate variabelen
    private enum State {idle, run, jumping, falling, hurt}
    private State state = State.idle;

    // de prive maar ook zichtbare variabelen
    [SerializeField] private LayerMask ground;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private int cherries = 0;
    [SerializeField] private TextMeshProUGUI cherryText;
    [SerializeField] private float hurtForce = 10f;
    [SerializeField] private AudioSource cherryS;
    [SerializeField] private AudioSource footstep;
    [SerializeField] private int Health;
    [SerializeField] private Text Lives;


    private void Start()
    {   // het oproepen van deze componeten
        rb = GetComponent<Rigidbody2D>();
        animatie = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        // de text ophalen
        Lives.text = Health.ToString();
    }



    // Update is called once per frame
    private void Update()
    {   
        if(state != State.hurt)
        {
            Movement();
        }
        AnimatioState();
        animatie.SetInteger("state", (int)state);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {   // een voorwerp op pakken voor punten
        if(collision.tag == "collectable")
        {
            cherryS.Play();
            Destroy(collision.gameObject);
            cherries += 1;
            cherryText.text = cherries.ToString();
        }
        // een object dat een extra leven geeft
        if(collision.tag == "powerup")
        {
            Destroy(collision.gameObject);
            GettingLives();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "enemy")
        {   // een jump boost krijgen als je op een enemie springt    
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            if(state == State.falling)
            {
                enemy.JumpedOn();
                Jump();
            }
            else
            {   // als je door een enemie geraakt word verlies je een leven
                state = State.hurt;
                LoseingLive();
                // als de enemie je raakt word je door hem weg gestoten
                if (other.gameObject.transform.position.x > transform.position.x)
                {
                    rb.velocity = new Vector2(-hurtForce, rb.velocity.y);
                }
                else
                {
                    rb.velocity = new Vector2(hurtForce, rb.velocity.y);
                }
            }
        }
    }

    private void LoseingLive()
    {   // hier door gaan je levens naar beneden
        Health -= 1;
        Lives.text = Health.ToString();
        if (Health <= 0)
        {   
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void GettingLives()
    {   // hier door kun je weer een leven krijgen
        Health += 1;
        Lives.text = Health.ToString();
    }

    private void Movement()
    {
        float hDirection = Input.GetAxis("Horizontal");
        // naar links kijken en bewegen
        if (hDirection < 0)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            transform.localScale = new Vector2(-1, 1);
        }
        // naar rechts kijken en bewegen
        else if (hDirection > 0)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            transform.localScale = new Vector2(1, 1);
        }
        // springen en kijke of je de grond aan raakt
        if (Input.GetButtonDown("Jump") && coll.IsTouchingLayers(ground))
        {
            Jump();
        }
    }
    private void Jump()
    {   // hoe de jump werkt
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        // de animatie word gecheked of je aan het springen bent
        state = State.jumping;
    }

    private void AnimatioState()
    {   // als de animatie jump is geweest dan gaat hij naar de animatie val
        if(state == State.jumping)
        {
            if(rb.velocity.y < .1f)
            {
                state = State.falling;
            }
        }
        // als hij is gevallen en hij raakt de groen ga dan naar idle
        else if (state == State.falling)
        {
            if(coll.IsTouchingLayers(ground))
            {
                state = State.idle;
            }
        }
        // als je geraakt bent ga dan naar idle
        else if(state == State.hurt)
        {
            if(Mathf.Abs(rb.velocity.x) < .1f)
            {
                state = State.idle;
            }
        }
        // alles sneller dan 2 is rennen
        else if(Mathf.Abs(rb.velocity.x) > 2f)
        {
            state = State.run;
        }
        else
        // anders staat ik stil
        {
            state = State.idle;
        }
    }

    private void Footstep()
    {
        // audio voetstappen
        footstep.Play();

    }

   
}
