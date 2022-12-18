using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Headlight : MonoBehaviour
{
    [SerializeField] private GameObject Sphere;
    [SerializeField] private Camera Camera;
    private Vector3 shift;
    void Start()
    {
        shift = this.transform.position - Sphere.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = Sphere.transform.position + shift;
        this.transform.rotation = Camera.transform.rotation;
    }
}
