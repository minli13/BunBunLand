using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : PhysicsBase
{
    public float edgeCheckDistance = 0.5f;
    public LayerMask groundLayer;

    // Start is called before the first frame update
    void Start()
    {
        desiredx = 3;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void CollideHorizontal(Collider2D other)
    {
        desiredx = -desiredx; 

        // Hit player, deal damage
        if (other.gameObject.CompareTag("Player"))
        {
            desiredx = -desiredx;
            other.GetComponent<PlayerController>().TakeDamage(1, transform);
        }
        

    }


}
