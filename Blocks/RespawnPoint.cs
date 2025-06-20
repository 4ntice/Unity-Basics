using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    public GameObject player;

    [SerializeField] float saveInterval = 1.0f;
    float dTimer = 0.0f;

    [SerializeField] float checkRadius = 1.0f;

    public Vector3 safePoint = Vector3.zero;
    public Quaternion safeRot = Quaternion.identity;
    
    int invalidCount = 0;
    void Start()
    {
        ////There are more children than colliders, it's esier to just set the value at this point
        //foreach (Transform t in player.transform)
        //{
        //    invalidCount++;
        //    Debug.Log(t.name);

        //    //if (t.GetComponent<Collider>())
        //    //{

        //    //}
        //}

        invalidCount = 2; //set to total ammount of colliders in player

        //Debug.Log("invalidCount = " + invalidCount);
    }

    void FixedUpdate()
    {
        dTimer += Time.fixedDeltaTime;
        if(dTimer > saveInterval)
        {
            if(Vector3.Distance(safePoint, player.transform.position) < checkRadius)
            {
                return;
            }

            dTimer = 0.0f;

            CheckSafePoint();
        }
    }

    public void RespawnNow()
    {
        player.transform.position = safePoint;
        player.transform.rotation = safeRot;
    }

    void CheckSafePoint()
    {
        Collider[] colliders = Physics.OverlapSphere(player.transform.position, checkRadius);
        int validCount = colliders.Length - invalidCount;


        foreach (Collider collider in colliders)
        {
            Debug.Log(collider.name);
        }

        Debug.Log("ValidCount = " + validCount);

        if (validCount > 0)
        {
            return;
        }

        safePoint = player.transform.position;
        safeRot = player.transform.rotation;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(safePoint, checkRadius);
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawSphere(safePoint, checkRadius);
        Gizmos.color = Color.yellow;
    }
}
