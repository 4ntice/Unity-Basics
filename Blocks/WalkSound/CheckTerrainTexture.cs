using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTerrainTexture : MonoBehaviour
{
    public Transform playerTransform;
    public Terrain t;
    public int posX;
    public int posZ;
    public float[] textureValues;

    void Start()
    {
        t = Terrain.activeTerrain;
        playerTransform = gameObject.transform;
    }

    public CharacterController cc;
    public float distance;
    void Update()
    {
        // For better performance, move this out of update 
        // and only call it when you need a footstep.
        GetTerrainTexture();

        distance += cc.velocity.magnitude * Time.deltaTime;
        if(distance >= 2.0f && cc.isGrounded)
        {
            PlayFootstep();
            distance = 0.0f;
        }
    }
    public void GetTerrainTexture()
    {
        ConvertPosition(playerTransform.position);
        CheckTexture();
    }
    void ConvertPosition(Vector3 playerPosition)
    {
        Vector3 terrainPosition = playerPosition - t.transform.position;
        Vector3 mapPosition = new Vector3
        (terrainPosition.x / t.terrainData.size.x, 0,
        terrainPosition.z / t.terrainData.size.z);
        float xCoord = mapPosition.x * t.terrainData.alphamapWidth;
        float zCoord = mapPosition.z * t.terrainData.alphamapHeight;
        posX = (int)xCoord;
        posZ = (int)zCoord;
    }
    void CheckTexture()
    {
        float[,,] aMap = t.terrainData.GetAlphamaps(posX, posZ, 1, 1);
        textureValues[0] = aMap[0, 0, 0];
        textureValues[1] = aMap[0, 0, 1];
        textureValues[2] = aMap[0, 0, 2];
        textureValues[3] = aMap[0, 0, 3];
    }

    public AudioSource source;
    public AudioClip[] stoneClips;
    public AudioClip[] dirtClips;
    public AudioClip[] sandClips;
    public AudioClip[] grassClips;
    AudioClip previousClip;
    public void PlayFootstep()
    {
        GetTerrainTexture();
        if (textureValues[0] > 0)
        {
            source.PlayOneShot(GetClip(stoneClips), textureValues[0]);
        }
        if (textureValues[1] > 0)
        {
            source.PlayOneShot(GetClip(dirtClips), textureValues[1]);
        }
        if (textureValues[2] > 0)
        {
            source.PlayOneShot(GetClip(dirtClips), textureValues[2]);
        }
        if (textureValues[3] > 0)
        {
            source.PlayOneShot(GetClip(dirtClips), textureValues[3]);
        }
    }
    AudioClip GetClip(AudioClip[] clipArray)
    {
        int attempts = 3;
        AudioClip selectedClip =
        clipArray[Random.Range(0, clipArray.Length - 1)];
        while (selectedClip == previousClip && attempts > 0)
        {
            selectedClip =
            clipArray[Random.Range(0, clipArray.Length - 1)];

            attempts--;
        }
        previousClip = selectedClip;
        return selectedClip;
    }
}
