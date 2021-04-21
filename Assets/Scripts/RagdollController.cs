using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollController : MonoBehaviour
{
    private HashSet<Rigidbody> rigidbodies = new HashSet<Rigidbody>();
    void Start()
    {
        Rigidbody[] rb = gameObject.GetComponentsInChildren<Rigidbody>();
        foreach (var rigidbody1 in rb)
            rigidbodies.Add(rigidbody1);

        Collider[] colliders = gameObject.GetComponentsInChildren<Collider>();
        foreach (var collider1 in colliders)
            collider1.gameObject.layer = 8;
        
        RagdollOFF();
    }

    public void RagdollON()
    {
        foreach (var rb in rigidbodies)
        {
            rb.isKinematic = false;
            float multiplies = Random.Range(300, 400);
            rb.AddForce(Vector3.up * multiplies);
        }
    }

    public void RagdollOFF()
    {
        foreach (var rb in rigidbodies)
        {
            rb.isKinematic = true;
        }
    }
}
