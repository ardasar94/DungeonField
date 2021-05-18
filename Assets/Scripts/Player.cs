using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] Animator animator;

    Rigidbody2D myRigidbody;
    BoxCollider2D myFeet;
    CapsuleCollider2D myBodyCollider;
    float gravityScaleAtStart;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myFeet = GetComponent<BoxCollider2D>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        gravityScaleAtStart = myRigidbody.gravityScale;
    }

    void Update()
    {
        if (IsAlive())
        {
            Move();
            Jump();
            Attack();
            ClimbLadder();
        }
        else
        {
            Die();
        }
    }

    private void Die()
    {
        animator.SetTrigger("isDead");
        Destroy(gameObject, 2);
        gameObject.layer = 10;
        FindObjectOfType<GameSession>().ProcessPlayerDeath();

    }

    private void Attack()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerAttack")) { return; }

        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetBool("isAttacking", true);
        }
    }

    private void Jump()
    {
        if (!myFeet.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            animator.SetBool("isJumping", true);
            return;
        }
        else
        {
            animator.SetBool("isJumping", false);
        }
        if (Input.GetButtonDown("Jump"))
        {
            Vector2 JumpVelocity = new Vector2(0, jumpSpeed);
            myRigidbody.velocity += JumpVelocity;
        }
    }

    private void ClimbLadder()
    {
        if (!myFeet.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            animator.SetBool("isClimbing", false);
            myRigidbody.gravityScale = gravityScaleAtStart;
            return;
        }
        myRigidbody.gravityScale = 0;
        var controlThrow = Input.GetAxis("Vertical");
        Vector2 climbVelocity = new Vector2(myRigidbody.velocity.x, controlThrow * climbSpeed);
        myRigidbody.velocity = climbVelocity;
        animator.SetBool("isClimbing", true);
    }

    private void Move()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerAttack")) { return; }
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var posX = transform.position.x + deltaX;
        var posY = transform.position.y;

        bool isPLayerRunning = Mathf.Abs(deltaX) > Mathf.Epsilon;
        animator.SetBool("isRunning", isPLayerRunning);



        transform.position = new Vector2(posX, posY);
        if (Mathf.Abs(deltaX) > Mathf.Epsilon)
        {
            transform.localScale = new Vector2(Mathf.Sign(deltaX), 1f);
        }

    }

    public void StopAttacking()
    {
        animator.SetBool("isAttacking", false);
    }

    public void AttackDamage(int x)
    {
        FindObjectOfType<PlayerAttack>().DealDamage(x);
        transform.position = new Vector2(transform.position.x + 0.01f, transform.position.y);
    }

    public bool IsAlive()
    {
        return GetComponent<PlayerHealth>().ShowHealth() > 0 && !myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Hazards"));
    }

}
