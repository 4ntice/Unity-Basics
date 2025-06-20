using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SphereCollider))]
public class InfoPointer : MonoBehaviour
{
    [SerializeField, SaveThis] bool isDiscovered = false;

    public float detectRadius = 1.0f;
    public bool isTripped = false;
    //bool wasTripped = false;

    public UnityEvent enterEvent;
    public UnityEvent exitEvent;

    public string playerName = "Plane";

    SphereCollider col;

    public Material[] iconMaterial = new Material[2];
    ParticleSystem iconParticle;

    void Awake()
    {
        if(col == null)
        {
            col = GetComponent<SphereCollider>();
        }
        col.isTrigger = true;
        col.radius = detectRadius;

        if (iconParticle == null)
        {
            iconParticle = GetComponentInChildren<ParticleSystem>();//we need the color of the particles
        }
        if(iconMaterial[0] == null)
        {
            iconMaterial[0] = iconParticle.GetComponent<Material>();//the first should always be the one for enabled
        }

        if (isDiscovered)
        {
            iconParticle.GetComponent<ParticleSystemRenderer>().material = iconMaterial[0];
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == playerName)
        {
            isDiscovered = true;
            enterEvent.Invoke();
            //iconMaterial.SetColor("_SpecColor", Color.gray); //sets the material even after exit for all objects with it
            iconParticle.GetComponent<ParticleSystemRenderer>().material = iconMaterial[1];   //set to disabled material
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == playerName)
        {
            exitEvent.Invoke();
        }
    }

    //void Update()
    //{
    //    Collider[] colls = Physics.OverlapSphere(transform.position, detectRadius);//this is broken

    //    if (isTripped)
    //    {
    //        enterEvent.Invoke();
    //    }
    //    else
    //    {
    //        exitEvent.Invoke();
    //    }

    //    if (colls == null)
    //    {
    //        return;
    //    }

    //    foreach (Collider coll in colls)
    //    {
    //        //Debug.Log(coll);
    //        if (coll.gameObject.name == playerName)
    //        {
    //            isTripped = true;

    //            //return;
    //        }
    //        else
    //        {
    //            isTripped = false;
    //            //Debug.Log(coll);
    //        }
    //    }

    //    colls = null;

    //    if (isTripped == wasTripped)
    //    {
    //        return;
    //    }

    //    wasTripped = isTripped;
    //}


    private void OnDrawGizmos()
    {
    //    Gizmos.color = Color.green * new Color(1, 1, 1, 0.5f);
    //    if (isTripped)
    //    {
    //        Gizmos.color = Color.red * new Color(1, 1, 1, 0.5f);
    //    }

    //    Gizmos.DrawSphere(transform.position, detectRadius);
    //    Gizmos.DrawWireSphere(transform.position, detectRadius);
    
            Gizmos.color = Color.green * new Color(1, 1, 1, 0.5f);
            Gizmos.DrawSphere(transform.position, detectRadius);
    }
}
