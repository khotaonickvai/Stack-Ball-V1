using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncing : MonoBehaviour
{
    private Rigidbody rb;
    private const float relativeVelocity = 12f;
    private const float lacalScaleRelative = 3f;
    private float maxSpeed = 2;
    public const float relativeLocalPosition = 1f;
    private Vector3 positionCatch;
    private ParticleSystem bouncingFx;
    private Ball ball;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        positionCatch = new Vector3(transform.localScale.x,0, transform.localScale.z);
        bouncingFx = GetComponentInChildren<ParticleSystem>();
        ball = GetComponentInParent<Ball>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A)&&ball.IsInGame())
        {
            DisableRigid();
           
            transform.localPosition = Vector3.up * relativeLocalPosition;
        }
        else
        {
            Bound(); 
        }

        float normV = rb.velocity.y / maxSpeed;
        if (Mathf.Abs(normV) > Mathf.Epsilon)
        {
            normV /= lacalScaleRelative;
        }

        if (rb.velocity.y >= 0)
        {
            positionCatch.y = 1 + normV;
            transform.localScale = positionCatch;
        }
        
        GetMaxSpeed();

    }

    private void Bound()
    {
        EnableRigid();
        if (transform.localPosition.y <= relativeLocalPosition)
        {
            rb.velocity = Vector3.up * relativeVelocity;
            if (!bouncingFx.isPlaying)
            {
               bouncingFx.Play();
                
            }
        }

        if (Input.GetKey(KeyCode.D))
        {
            Debug.Log(transform.localPosition);
        }
    }
    private void EnableRigid()
    {
        rb.useGravity = true;
        rb.isKinematic = false;
        rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
    }

    private void DisableRigid()
    {
        rb.useGravity = false;
        rb.useGravity = true;
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }

    private float GetMaxSpeed()
    {
        if (maxSpeed < rb.velocity.y)
        {
            maxSpeed = rb.velocity.y;
        }

        return maxSpeed;
    }
}
