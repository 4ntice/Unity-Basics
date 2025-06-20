using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnPointControl : MonoBehaviour
{
    public GameObject player;
    public SpawnPoint[] spawnPoints;

    public Camera mapCamera;
    public Canvas canvas;
    public Button[] spawnButton;

    private void Start()
    {
        if(mapCamera == null)
        {
            mapCamera = new Camera();
            mapCamera.transform.position = Vector3.zero;
            mapCamera.transform.forward = Vector3.down;
            //mapCamera.projectionMatrix = Matrix4x4.Ortho(250,250,250,250,200,1); //WTF is this, need to educate myself
            mapCamera.orthographic = true;
            mapCamera.orthographicSize = 500;
        }

        foreach(SpawnPoint point in spawnPoints)
        {
            point.player = player;
        }
    }

    //
    // Summary:
    //     Checks a equal lenth face axis cube
    bool isInside(Vector3 borders, Vector3 point)
    {
        if(borders.x < Mathf.Abs(point.x))
        {
            return false;
        }

        if(borders.y < Mathf.Abs(point.y))
        {
            return false;
        }

        if(borders.z < Mathf.Abs(point.z))
        {
            return false;
        }

        return true;
    }

    public void SetSpawn()
    {

    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Vector3 myPoint = transform.position;

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            Gizmos.DrawLine(myPoint, spawnPoints[i].transform.position);
            spawnPoints[i].transform.name = null;
            string nameWish = "SpawnPoint " + i;
            spawnPoints[i].transform.name = nameWish;
        }


        //foreach (SpawnPoint point in spawnPoints)
        //{
        //}
    }
}
