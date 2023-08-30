using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour, IObjectSpawn
{
    public float predictionsStepsPerFrame = 6.0f;

    public Vector3 bulletVelocity;

    public float speed;

    // Start is called before the first frame update
    //void Start()
    //{
    //    bulletVelocity = this.transform.forward * speed;
    //}

    public void OnObjectSpawn()
    {
        bulletVelocity = this.transform.forward * speed;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 point1 = this.transform.position;
        float stepSize = 1.0f / predictionsStepsPerFrame;
        for(float step  = 0; step < 1; step += stepSize)
        {
            bulletVelocity += Physics.gravity * stepSize * Time.deltaTime;
            Vector3 point2 = point1 + bulletVelocity * stepSize * Time.deltaTime;

            Ray ray = new Ray(point1, point2 - point1);
            if(Physics.Raycast(ray, (point2 - point1).magnitude))
            {
                Debug.Log("hit");
                this.gameObject.SetActive(false); break;
                //Destroy(this); break ;
            }
            transform.GetChild(0).gameObject.transform.LookAt(point2);

            point1 = point2;
        }
        this.transform.position = point1;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 point1 = this.transform.position;
        Vector3 predictedBulletVelocity = bulletVelocity;
        float stepSize = 0.01f;
        for(float step = 0.0f; step < 1; step += stepSize)
        {
            predictedBulletVelocity += Physics.gravity * stepSize;
            Vector3 point2 = point1 + predictedBulletVelocity * stepSize;
            Gizmos.DrawLine(point1, point2);
            point1 = point2;
        }
    }
}
