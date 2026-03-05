using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpManager : MonoBehaviour
{   
    //Create references for both popups
    public GameObject infoPopup;
    public GameObject comingSoonPopup;

    //Turn on the information panel
    public void ShowInfo()
    {   
        //set active the pop up for the information text
        infoPopup.SetActive(true);
    }

        //Turn on the coming soon panel
    public void ShowComingSoon()
    {   
        //set active the popup for the coming soon text
        comingSoonPopup.SetActive(true);
    }

    void Update()
    {
        //Check if user clicks anywhere
        if (Input.GetMouseButtonDown(0))
        {
            //disable both popups
            infoPopup.SetActive(false);
            comingSoonPopup.SetActive(false);
        }
    }
}
