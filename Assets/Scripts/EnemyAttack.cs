using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    
    EdgeCollider2D myRange;
    GameObject parentEnemy;
    // Start is called before the first frame update
    void Start()
    {
        
        myRange = GetComponent<EdgeCollider2D>();
        parentEnemy = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        parentEnemy.GetComponent<Enemy>().Attack(myRange);
    }
}
