using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float enemyMoveSpeed = 3f;
    [SerializeField] Animator animator;

    float enemyRoute = -1f;
    Rigidbody2D myRigidbody;
    bool shouldStop;
    GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        shouldStop = false;
        player = FindObjectOfType<PlayerHealth>().gameObject;

    }

    // Update is called once per frame
    void Update()
    {
        Walk();
    }

    private void Walk()
    {
        if (shouldStop) return;
        animator.SetBool("isWalking", !shouldStop);
        myRigidbody.velocity = new Vector2(enemyMoveSpeed * enemyRoute, 0f);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.gameObject.GetComponent<Player>())
        {
            enemyRoute = -enemyRoute;
            transform.localScale = new Vector2(-enemyRoute, 1);
        }
    }

    public void Attack(EdgeCollider2D myRange)
    {
        if (myRange.IsTouchingLayers(LayerMask.GetMask("Player")) && FindObjectOfType<PlayerHealth>().ShowHealth() > 0)
        {
            myRigidbody.velocity = new Vector2(0f, 0f);
            animator.SetBool("isWalking", false);
            animator.SetBool("isAttacking", true);
            transform.position = new Vector2(transform.position.x + 0.000001f, transform.position.y);
            shouldStop = true;

        }
        else if (!(myRange.IsTouchingLayers(LayerMask.GetMask("Player"))) || FindObjectOfType<PlayerHealth>().ShowHealth() <= 0)
        {
            animator.SetBool("isAttacking", false);
            myRigidbody.velocity = new Vector2(enemyMoveSpeed * enemyRoute, 0f);
            if (shouldStop)
            {
                shouldStop = false;
            }

        }
    }

    public void DealDamage(int x)
    {
        player.GetComponent<PlayerHealth>().GetDamage(x);
    }
}
