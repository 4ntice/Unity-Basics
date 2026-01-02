using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalMaster : MonoBehaviour, ISaveAble
{
    public static int destroyIndex = -1; //set all bits to 1
    [SaveThis] public int destroySave = -1;
    public static int houseCount = 1;
    public SaveSystem saveSystem;

    private void OnEnable()
    {
        saveSystem.LoadGame();
        destroyIndex = destroySave;

        Debug.Log("destroyIndex " + destroyIndex);
        Debug.Log("houseCount " + houseCount);

        //Debug.Log("Some text/n new line?");

        
        //destroyIndex = 0;
    }

    void FixedUpdate()
    {

    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) //stupid idea to save before tabbing out
        {
            destroySave = destroyIndex;
            saveSystem.SaveGame();
        }
    }
}
