using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHandler : MonoBehaviour
{
    public List<GameObject> menuOptionList;

    private void Start()
    {
        SetMenuScree(0);
    }

    public void SetMenuScree(int menuListID)
    {
        menuOptionList[menuListID].SetActive(true);

        for (int i = 0; i < menuOptionList.Count; i++)
        {
            menuOptionList[i].SetActive(false);

            if(i == menuListID)
            {
                menuOptionList[menuListID].SetActive(true);
            }
        }
    }
}
