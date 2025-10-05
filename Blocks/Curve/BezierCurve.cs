using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BezierCurve : MonoBehaviour
{
    public Transform[] points;

    public Vector3 inbetween;
    [Range(0f, 1f)]
    public float speed = 1.0f; 
    [Range(0f, 1f)]
    public float step;
    public int segment;
    void Update()
    {
        if(points.Length >= 1)
        {
            
        }
    }

    void OnDrawGizmos()
    {
        if(step >= 1.0f)
        {
            step = 0.0f;
            segment++;
        }
        else
        {
            step += 0.01f * Time.deltaTime * speed;
        }

        if(segment >= points.Length - 1)
        {
            segment = 0;
        }

        // to - from = p1 - p0
        //a + (b - a) * t
        //This is a lerp
        for(int i = 0; i < points.Length - 1; i++)
        {
            inbetween = points[i].position + (points[i + 1].position - points[i].position) * step;
            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(inbetween, 0.1f);
        }
        Vector3 P = Vector3.zero;
        Vector3 P0 = points[0].position;
        Vector3 P1 = points[1].position;
        Vector3 P2 = points[2].position;
        Vector3 P3 = points[3].position;
        float t = step;

        P = P0 +
               t * (-3 * P0 + 3 * P1) +
               t * t * (3 * P0 - 6 * P1 + 3 * P2) +
               t * t * t * (-P0 + 3 * P1 - 3 * P2 + P3);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(P, 0.1f);

        //Vector3[] path = new Vector3[15];
        //for(int i = 0; i < path.Length - 1; i++)
        //{
        //    float f =  i / path.Length;

        //    path[i] = P0 +
        //       f * (-3 * P0 + 3 * P1) +
        //       f * f * (3 * P0 - 6 * P1 + 3 * P2) +
        //       f * f * f * (-P0 + 3 * P1 - 3 * P2 + P3);
        //}
        //for(int i = 0; i < path.Length - 1; i++)
        //{
        //    Gizmos.DrawLine(path[i], path[i + 1]);
        //}

        //inbetween = points[segment].position + (points[segment + 1].position - points[segment].position) * step;

        Gizmos.color = Color.green;
        //Gizmos.DrawWireSphere(inbetween, 0.1f);

        for(int i = 0; i < points.Length; i++)
        {
            Gizmos.DrawWireSphere(points[i].position, 0.1f);
            if(i < points.Length - 1)
            {
                Gizmos.DrawLine(points[i].position, points[i + 1].position);
            }
        }
    }
}
