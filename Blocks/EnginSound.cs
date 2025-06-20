using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnginSound : MonoBehaviour
{
    public AudioSource aS;

    public AudioClip clip;
    public float clipTimeOffset;

    public float dTimer;
    public float pitchChange;
    public float maxPitch = 1.1f;
    public float minPitch = 0.9f;

    public float soundPitchOffset = 0;

    //public VehicleDrive vehicleDrive;
    void Start()
    {
        if(clipTimeOffset == 0)
        {
            clipTimeOffset = clip.length * 0.5f;
        }
    }

    void Update()
    {
        float standardPitch = 0.0f;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            standardPitch = maxPitch;
        }
        if (Input.GetKey(KeyCode.LeftControl))
        {
            standardPitch = minPitch;
        }

        pitchChange = Mathf.Lerp(pitchChange, standardPitch, Time.deltaTime);

        //pitchChange = Mathf.Abs(Input.GetAxis("Vertical") * maxPitch);

        dTimer += Time.deltaTime;
        if(dTimer > clip.length - clipTimeOffset)
        {
            aS.pitch = 1 + pitchChange + soundPitchOffset;
            aS.PlayOneShot(clip);
            dTimer = 0;
        }
    }
}
