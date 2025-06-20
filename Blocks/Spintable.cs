using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spintable : MonoBehaviour
{
    public GameObject targetObject;
    float dTimer01 = 0;
    public float rotationSpeed = 10;

    void Start()
    {
        if(targetObject == null)
        {
            targetObject = this.gameObject;
        }
    }

    void FixedUpdate()
    {
        dTimer01 += Time.deltaTime;

        if (dTimer01 >= 360)
        {
            dTimer01 = 0;
        }

        Vector3 targetRotation = Vector3.up * dTimer01 * rotationSpeed;
        targetObject.transform.rotation = Quaternion.Euler(targetRotation);
    }
}
