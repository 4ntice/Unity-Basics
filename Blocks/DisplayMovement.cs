using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayMovement : MonoBehaviour
{
    public float rotationsSpeed = 1;
    public bool x = false;
    public bool y = true;
    public bool z = false;
    public bool useLocalRotation = false;

    public Transform parent;

    private void Start()
    {
        if (transform.parent)
        {
            parent = transform.parent.transform;
        }
    }

    private void Update()
    {
        if (useLocalRotation)
        {
            transform.localRotation *= Quaternion.Euler(RotationMagnitude() * Time.deltaTime * rotationsSpeed);
        }
        else
        {
            transform.rotation = Quaternion.Euler(RotationMagnitude() * Time.deltaTime * rotationsSpeed);
        }
    }

    Vector3 RotationMagnitude()
    {
        Vector3 rotateVector = Vector3.zero;

        if(x)
        {
            rotateVector.x = 1;
        }
        if(y)
        {
            rotateVector.y = 1;
        }
        if (z)
        {
            rotateVector.z = 1;
        }

        return rotateVector;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        if (useLocalRotation)
        {
            Vector3 visualVector = Vector3.zero;
            if (x)
            {
                visualVector += transform.right.normalized;
            }
            if (y)
            {
                visualVector += transform.up.normalized;
            }
            if (z)
            {
                visualVector += transform.forward.normalized;
            }

            Gizmos.DrawRay(transform.position, visualVector);
        }
        else
        {
            Gizmos.DrawRay(transform.position, RotationMagnitude());
        }
    }
}
