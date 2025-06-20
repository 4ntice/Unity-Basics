using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ArrayCloneObject : MonoBehaviour
{
    [Range(0,99)]
    public int ammount = 1;
    public GameObject objectToClone;

    //public class Offset
    //{
    //    public Vector3 position;
    //}

    public Vector3 offset = Vector3.one;

    public List<GameObject> objectsInArray = new List<GameObject>();

    int objectsCount;

    Renderer rend;
    Vector3 objectSize;
    Vector3 objectCenter;

    public enum OffsetType
    {
        constant,
        relativ
    }
    public OffsetType offsetType = OffsetType.relativ;
    void OnEnable()
    {
        if (!objectToClone)
        {
            objectToClone = this.gameObject;
            ArrayCloneObject arrayCloneObject = objectToClone.GetComponent<ArrayCloneObject>();
            DestroyImmediate(arrayCloneObject);
        }

        if (!rend)
        {
            try
            {
                rend = objectToClone.transform.GetComponent<Renderer>();
                rend = objectToClone.transform.GetComponent<Renderer>();
                objectSize = rend.bounds.size;
                objectCenter = rend.bounds.center;
            }
            catch
            {

            }
        }
    }

    void Update()
    {
        CloneInstances();

        ClonesOffset();
    }

    void ClonesOffset()
    {
        if (objectsCount <= 1)
        {
            return;
        }
        Vector3 objectOffset = offset;
        switch (offsetType)
        {
            case OffsetType.constant:

                break;
            case OffsetType.relativ:
                 objectOffset = Vector3.Scale(offset, objectSize);
                break;
        }

        for (int i = 1; i < objectsCount; i++) //ignore the first object
        {
            if(objectsInArray.Count <= 0)
            {
                return;
            }
            GameObject go = objectsInArray[i];
            go.transform.position = transform.position + (i * objectOffset);
        }
    }

    void CloneInstances()
    {
        objectsCount = 0;
        if (objectsInArray.Count != null) //do not care, just want to make sure. it will work with the previous line but again I want to be sure
        {
            objectsCount = objectsInArray.Count;
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

        for (int i = objectsCount; i < ammount; i++) // add to the list
        {
            GameObject go = Instantiate(objectToClone, this.transform);
            go.transform.position = transform.position + (i * offset);
            objectsInArray.Add(go);
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 objectOffset = offset;
        Vector3 objectPosition = transform.position;

        Gizmos.color = Color.blue;//the render bounding box
        Gizmos.DrawWireCube(transform.position + objectCenter, objectSize);

        Gizmos.color = Color.green;//the offset bounding box

        switch (offsetType)
        {
            case OffsetType.constant:

                break;
            case OffsetType.relativ:
                Vector3 relativeSizeToPosition = Vector3.Scale(objectCenter, (objectCenter - transform.position));// I want to make this box corner the mesh of the next cloneObject


                objectOffset = Vector3.Scale(offset, objectSize);
                objectPosition = transform.position + Vector3.Scale(offset, objectCenter);
                break;
        }

        Gizmos.DrawWireCube(objectPosition, objectOffset);
    }
}
