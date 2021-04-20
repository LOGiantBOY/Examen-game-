using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class frog : Enemy
{
   [SerializeField] private float leftCap;
   [SerializeField] private float rihgtCap;
    [SerializeField] LayerMask ground;
    private Collider2D coll;


    [SerializeField] private float jumpLenght = 10f;
    [SerializeField] private float jumpHeight = 15f;

    private bool facingLeft = true;


    protected override void Start()
    {
        base.Start();
        coll = GetComponent<Collider2D>();
    }

    private void Update()
    {   // kijken of de frog springt of valt
        if (anim.GetBool("Jumping"))
        {
            if (rb.velocity.y < .1)
            {
                anim.SetBool("Falling", true);
                anim.SetBool("Jumping", false);
            }
        }
        if (coll.IsTouchingLayers(ground) && anim.GetBool("Falling"))
        {
            anim.SetBool("Falling", false);

        }
    }

    private void Move()
    {   // dat hij naar rechts toe springt
        if (facingLeft)
        {
            if (transform.position.x > leftCap)
            {
                if (transform.localScale.x != 1)
                {
                    transform.localScale = new Vector3(1, 1);
                }
                if (coll.IsTouchingLayers(ground))
                {
                    rb.velocity = new Vector2(-jumpLenght, jumpHeight);
                    anim.SetBool("Jumping",true);
                }
            }
            else
            {
                facingLeft = false;
            }
        }

        else
        {   // dat hij naar links springt
            if (transform.position.x < rihgtCap)
            {
                if (transform.localScale.x != -1)
                {
                    transform.localScale = new Vector3(-1, 1);
                }
                if (coll.IsTouchingLayers(ground))
                {
                    rb.velocity = new Vector2(jumpLenght, jumpHeight);
                    anim.SetBool("Jumping", true);

                }
            }
            else
            {
                facingLeft = true;
            }
        }
    }

   
}
