using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class FollowCurveArray : MonoBehaviour
{
    public TrainPath trainPath;
    public GameObject copyObject;
    public Vector3 boundingBox;

    int currentWaypoint = 0;
    float wayLength = 0;

    public int objectsCount = 1;
    public List<GameObject> objectsInArray = new List<GameObject>();
    private void Start()
    {
        MeshRenderer meshRenderer = copyObject.GetComponent<MeshRenderer>();
        boundingBox = meshRenderer.bounds.size;
    }

    void Update()
    {
        CreateObjects();

        Follow();
    }
    void Follow()
    {
        Vector3 firstPos = trainPath.wayPoints[currentWaypoint].transform.position;

        Vector3 nextPos = Vector3.zero; //dirty fix had a better one once

        if (currentWaypoint + 1 > trainPath.wayPoints.Count - 1)
        {
            nextPos = trainPath.wayPoints[0].transform.position;
        }
        else
        {
            nextPos = trainPath.wayPoints[currentWaypoint + 1].transform.position;
        }

        transform.forward = firstPos - nextPos;

        if (wayLength < 0.99)
        {
            float distanceSpeed = 1 / Vector3.Distance(firstPos, nextPos);//same speed for distance
            //Debug.Log(distanceSpeed);

            wayLength += distanceSpeed * Time.deltaTime * (speed * maxSpeed);


            transform.position = Vector3.Lerp(firstPos, nextPos, wayLength);
            return;
        }

        currentWaypoint++;
        wayLength = 0;
        //Debug.Log(currentWaypoint);

        if (currentWaypoint > trainPath.wayPoints.Count - 1)
        {
            currentWaypoint = 0;
        }
    }

    void CreateObjects()
    {
        int ammount = objectsInArray.Count;

        if (objectsInArray.Count <= 0) //do not care, just want to make sure. it will work with the previous line but again I want to be sure
        {
            objectsCount = ammount;
        }

        if (objectsCount == ammount) //turn out if reached
        {
            return;
        }

        if (objectsCount > ammount)
        {
            for (int i = objectsCount; i > ammount; i--) // delete the last ones on the list
            {
                GameObject go = objectsInArray[i - 1]; // if the objectCount - 1 is used they start at 1 so 1 would equal 2 objects... this fixes it
                objectsInArray.Remove(go);
                DestroyImmediate(go);
            }
            return;
        }

        for (int i = ammount; i < objectsCount; i++) // add to the list
        {
            GameObject go = Instantiate(copyObject, this.transform);
            //go.transform.position = transform.position + (i * offset);
            objectsInArray.Add(go);
        }
    }
}
