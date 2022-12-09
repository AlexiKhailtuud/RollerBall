using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public float lifeTime = 3f;
    public float speed = 100f;
    private Rigidbody rb;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    public void Init()
    {
        rb.AddForce(transform.forward * 10f, ForceMode.Acceleration);
    }
    
    private void FixedUpdate()
    {
        rb.AddForce(transform.forward * speed, ForceMode.Acceleration);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
