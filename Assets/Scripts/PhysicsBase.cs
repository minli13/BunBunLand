using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsBase : MonoBehaviour
{
    public Vector2 velocity;
    public float gravityFactor;
    public float desiredx;
    public bool grounded;
    // Start is called before the first frame update
    void Start()
    {
        velocity = Vector2.zero;
        desiredx = 0f;
        grounded = false;
        gravityFactor = 1f; // can be changed in subclasses
    }

    public virtual void CollideHorizontal(Collider2D other) // override in subclasses (like PlayerController)
    {

    }

    public virtual void CollideVertical(Collider2D other) // override in subclasses (like PlayerController)
    {

    }

    void Movement(Vector2 move, bool horizontal) // moves and checks collisions
    {
        if (move.magnitude < 0.000001f) return; // ignore tiny movements
        grounded = false; // will be set to true if we land on something
        RaycastHit2D[] results = new RaycastHit2D[16]; // array to store results
        int cnt = GetComponent<Rigidbody2D>().Cast(move, results, move.magnitude + 0.01f); // check if we hit anything and store results
        bool collision = false;
        for (int i = 0; i < cnt; ++i) // for each collision, check normal and call appropriate function
        {
            if (Mathf.Abs(results[i].normal.x) > 0.3f && horizontal) // horizontal collision
            {
                collision = true; // stop movement
                CollideHorizontal(results[i].collider);
            }
            if (Mathf.Abs(results[i].normal.y) > 0.3f && !horizontal) // vertical collision
            {
                if (results[i].normal.y > 0.3f) grounded = true; // landed on something
                if (grounded) velocity.y = 0f; // stop downward velocity
                collision = true; // stop movement
                CollideVertical(results[i].collider); 
            }
        }

        if (collision) return; // if we collided, don't move
        transform.position += (Vector3)move; // no collision, so move

    }

    // Update is called once per frame
    void FixedUpdate() // physics updates
    {
        Vector2 acceleration = 9.81f * Vector2.down * gravityFactor;
        velocity += acceleration * Time.fixedDeltaTime; // apply gravity and add to velocity
        //transform.position += (Vector3)velocity * Time.deltaTime;
        velocity.x = desiredx; // set horizontal velocity directly
        Vector2 move = velocity * Time.fixedDeltaTime; // calculate movement this frame
        Movement(new Vector3(move.x, 0), true); // horizontal movement and collisions
        Movement(new Vector3(0, move.y), false); // vertical movement and collisions
    }
}