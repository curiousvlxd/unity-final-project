using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smiley : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject Sphere;
    private Vector3 shift;
    void Start()
    {
        shift = this.transform.position - Sphere.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = Sphere.transform.position + shift;
    }
}
