using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnableAndDisable : MonoBehaviour
{
    public List<MonoBehaviour> scripts;
    public List<GameObject> gameObjects;

    public void InvertStates()
    {
        if(scripts.Count >= 0)
        {
            foreach (MonoBehaviour script in scripts)
            {
                if (script != null)
                {
                    script.enabled = !script.enabled; // Toggle the active state
                }
            }
        }

        if (gameObjects.Count >= 0)
        {
            foreach (GameObject gameObject in gameObjects)
            {
                if (gameObject != null)
                {
                    gameObject.SetActive(!gameObject.activeInHierarchy); // Toggle the active state
                }
            }
        }
    }
}
