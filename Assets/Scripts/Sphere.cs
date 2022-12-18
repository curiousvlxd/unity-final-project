using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Sphere : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    private Rigidbody rb;
    private Vector3 jump = Vector3.up * 100;
    private Vector3 forceDirection;
    private const float FORCE_APML = 3;

    [SerializeField] private AudioSource collisionEffect;
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(jump);
        }
        float fx = Input.GetAxis("Horizontal");
        float fy = Input.GetAxis("Vertical");       // Vertical -> Z
        // абсолютний простір
        // rb.AddForce(new Vector3(fx, 0, fy) * 2);    // fy на позиції Z

        // відносно камери
        forceDirection = cam.transform.forward;     // напрям погляду камери
        forceDirection.y = 0;     // прибираємо вертикальну складову
        forceDirection = forceDirection.normalized * fy;  // довжина - 1
        forceDirection += cam.transform.right * fx;
        rb.AddForce(forceDirection * FORCE_APML);
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject.Destroy(other.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        collisionEffect.Play();
    }
}