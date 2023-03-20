using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class UsableObject : MonoBehaviour
{
    public Vector3 force;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody>().velocity = force;
    }
    private void OnCollisionEnter(Collision collision)
    {
        force = new Vector3();
    }
}
