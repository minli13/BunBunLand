using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
public class KnockBack : MonoBehaviour
{
    public float knockbackTime = 0.2f;
    public float hitDirectionForce = 10f;
    public float constForce = 5f;
    public float inputForce = 7.5f;
    private Rigidbody2D rb;

    public bool isKnockedBack = {
        get;
        private set; 
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    public IEnumerator KnockbackAction(Vector2 hitDirection, Vector2 constantForceDirection, float inputDirection)
    {
        isKnockedBack = true;
        Vector2 _hitForce;
        Vector2 _constantForce;
        Vector2 _knockbackForce;
        Vector2 _combinedForce;

        _hitForce = hitDirection * hitDirectionForce;
        _constantForce = constantForceDirection * constForce;

        float _elapsedTime = 0f;
        while (_elapsedTime < knockbackTime)
        {
            _elapsedTime += Time.deltaTime; // increment elapsed time
            _knockbackForce = _hitForce + _constantForce;
            
            if (inputDirection != 0)
            {
                _combinedForce = _knockbackForce + new Vector2(inputDirection, 0f);
            }
            else 
            {
                _combinedForce = _knockbackForce;
            }
            rb.velocity = _combinedForce;

            yield return new WaitForFixedUpdate();
        }
        
        isKnockedBack = false;
    }


}
*/