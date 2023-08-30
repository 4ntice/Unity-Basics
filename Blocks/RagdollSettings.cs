using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollSettings : MonoBehaviour
{
    public List<Collider> PlayerColliders = new List<Collider>();
    public List<Collider> RagdollParts = new List<Collider>();
    public List<Rigidbody> RagdollRigidBody = new List<Rigidbody>();

    private void Start()
    {
        Collider[] colliders = GetComponentsInChildren<Collider>();
        foreach (Collider c in colliders)
        {
            c.enabled = false;
            RagdollParts.Add(c);
        }

        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody r in rigidbodies)
        {
            r.isKinematic = true;
            RagdollRigidBody.Add(r);
        }

        foreach (Collider c in PlayerColliders)
        {
            c.isTrigger = false;
        }
    }
    public void TurnOnRaggdoll()
    {
        foreach(Collider c in PlayerColliders)
        {
            c.isTrigger = true;
        }

        foreach (Collider c in RagdollParts)
        {
            c.enabled = true;
        }

        foreach (Rigidbody r in RagdollRigidBody)
        {
            r.isKinematic = false;
        }
    }
}
