using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleForward : MonoBehaviour
{
    public float speed = 1.0f;
    public float radius = 1.0f;

    void Start()
    {
        
    }

    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;

        RaycastHit hit;
        Physics.SphereCast(transform.position, radius, transform.forward, out hit, radius);

        if (hit.transform)
        {
            Debug.Log(hit.transform.name);

            hit.transform.GetComponent<Health>().TakeDamage(100);

            Destroy(transform.gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
