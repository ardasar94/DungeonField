using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    //BoxCollider2D myRange;
    EdgeCollider2D myRange;
    bool shouldHit;
    // Start is called before the first frame update
    void Start()
    {
        //myRange = GetComponent<BoxCollider2D>();
        myRange = GetComponent<EdgeCollider2D>();
        shouldHit = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (myRange.IsTouchingLayers(LayerMask.GetMask("Enemy")) && shouldHit)
        {
            Debug.Log("vurdu");
            if (collision.gameObject.GetComponent<Enemy>())
            {
                Destroy(collision.gameObject);
            }
        }
    }

    public void DealDamage(int x)
    {
        if (x == 1)
        {
            shouldHit = true;
        }
        else if (x == 0)
        {
            shouldHit = false;
        }
    }
}
