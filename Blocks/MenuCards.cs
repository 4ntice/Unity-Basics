using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCards : MonoBehaviour
{
    public List<GameObject> menuCard;
    public int currentMenuCard = 0;

    private void Start()
    {
        //search for menu cars
        string thisName = gameObject.name;
        foreach(Transform t  in transform)
        {
            if(thisName == t.parent.name)
            {
                if (!menuCard.Contains(t.gameObject))
                {
                    menuCard.Add(t.gameObject);
                }
            }
        }

        SwitchCards();
    }
    public void SwitchCards(int switchTo = 0)
    {
        //Doing the actual switching part, looping through the options and enabel or disable cards
        //because I want to count up or down it has to be done in other voids until I know how to add variables in editor window

        //chekc if it's set up and more than one option is avaliable
        if(menuCard == null)
        {
            Debug.Log(transform.name + " : Set up menu cards under this object, it's empty.");
            return;
        }
        
        if(menuCard.Count <= 0)
        {
            Debug.Log(transform.name + " requires more than one menu card.");
            return;
        }

        //usual loop over check
        if(currentMenuCard < 0)
        {
            currentMenuCard = menuCard.Count;
        }

        if(currentMenuCard > menuCard.Count)
        {
            currentMenuCard = 0;
        }

        currentMenuCard = switchTo;

        //currentMenuCard += switchTo; //adding is stupid for control

        //if(isCountUp) //bool option can't backtrack multiple points or here the to the right card emediantly
        //{
        //    currentMenuCard++;
        //}
        //else
        //{
        //    currentMenuCard--;
        //}

        for(int i = 0; i < menuCard.Count; i++)
        {
            if(currentMenuCard == i)
            {
                menuCard[currentMenuCard].SetActive(true); //at this point it is the same thing i == currentMenuCard
            }
            else
            {
                menuCard[i].SetActive(false);
            }

            //if(currentMenuCard == i)
            //{
            //    menuCard[i].SetActive(true);
            //}
        }
    }

    public void SwitchToggle(int toggleCardNumber)
    {
        menuCard[toggleCardNumber].SetActive(!menuCard[toggleCardNumber].activeInHierarchy); //fuck you, that is why
    }
}
