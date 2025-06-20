using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBot : MonoBehaviour
{
    public float turnSpeed = 1.0f;
    public float moveSpeed = 1.0f;
    public List<Vector3> wayPoints = new List<Vector3>();
    public int currentWayPoint = 0;
    public float pointCheckRadius = 1.0f;

    void Start()
    {
        currentWayPoint = 0;
    }

    void Update()
    {       
        if(Vector3.Distance(transform.position, wayPoints[currentWayPoint]) < pointCheckRadius)
        {
            currentWayPoint++;
        }

        if(currentWayPoint > wayPoints.Count - 1)
        {
            currentWayPoint = 0;
        }

        transform.position += transform.forward * moveSpeed * Time.deltaTime;

        transform.forward = Vector3.Slerp(transform.forward, (wayPoints[currentWayPoint] - transform.position), turnSpeed * Time.deltaTime);

        //transform.LookAt(wayPoints[currentWayPoint]);
    }

    private void OnDrawGizmos()
    {
        foreach(Vector3 point in wayPoints)
        {
            Gizmos.DrawWireSphere(point, pointCheckRadius);
        }
    }
}
