using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Destrucable : MonoBehaviour, ISaveAble
{
    int myIndex;

    public GameObject normalObject;
    public GameObject damagedObject;

    public Rigidbody rb;

    public Vector3 lastCollisionVelocity;

    [Range(0f, 1f)]
    public float momentumKeepFactor = 0.5f;
    public float health = 1.0f;

    public AudioClip destructClip;
    public AudioClip damageClip;

    float dHealth = 0f;

    public AudioSource ad;

    float dTimer01 = 0f;
    bool carAlarm = false;
    int count = 0;

    void OnEnable()
    {
        myIndex = GlobalMaster.houseCount++;
    }

    void Start()
    {
        //Debug.Log($"{transform.gameObject.name} : { myIndex} : {this.transform.position}");

        dHealth = health;

        rb = GetComponent<Rigidbody>();
        //AssetDatabase.CreateAsset(destructClip, "../Sounds/ConcreteDrop.mp3");

        if (!IsIntact(myIndex))
        {
            transform.GetComponent<Collider>().enabled = false;
            normalObject.SetActive(false);
            damagedObject.SetActive(true);
        }
        else
        {
            normalObject.SetActive(true);
            damagedObject.SetActive(false);
        }
    }

    void Update()
    {
        if (carAlarm)
        {
            dTimer01 += Time.deltaTime;
            if(count > 12)
            {
                count = 0;
                carAlarm = false;
                return;
            }

            if(dTimer01 > damageClip.length + 0.1f)
            {
                dTimer01 = 0;
                count++;
                ad.PlayOneShot(damageClip);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        lastCollisionVelocity = collision.relativeVelocity;

        TakeDamage(lastCollisionVelocity.magnitude);

        if (health < 0)
        {
            //add keep momentum
            Rigidbody go = collision.gameObject.GetComponent<Rigidbody>();
            if(momentumKeepFactor < 1 )
            {
                go.velocity = lastCollisionVelocity * momentumKeepFactor;
            }

            transform.GetComponent<Collider>().enabled = false;

            return;
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if(health < 0)
        {
            MarkDestroyed(myIndex);

            ad.PlayOneShot(destructClip);
            normalObject.SetActive(false);
            damagedObject.SetActive(true);
            return;
        }

        if (dHealth != health)
        {
            ad.PlayOneShot(damageClip);
            dHealth = health;
            carAlarm = true;
        }
    }

    public void MarkDestroyed(int buildingId)
    {
        GlobalMaster.destroyIndex &= ~(1 << buildingId);
    }

    public void MarkIntact(int buildingId)
    {
        GlobalMaster.destroyIndex |= (1 << buildingId);
    }

    public bool IsIntact(int buildingId)
    {
        return (GlobalMaster.destroyIndex & (1 << buildingId)) != 0;
    }

}
