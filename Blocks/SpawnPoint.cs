using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    //public Vector3[] points;
    public GameObject player;

    void Start()
    {
        
    }

    public void Spawn()
    {
        player.transform.position = transform.position;
        player.transform.rotation = transform.rotation;
        player.SetActive(true);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow * new Color(1,1,1,0.5f);
        Gizmos.DrawSphere(transform.position, 5.0f);
        Gizmos.DrawWireSphere(transform.position, 5.0f);
    }
}
