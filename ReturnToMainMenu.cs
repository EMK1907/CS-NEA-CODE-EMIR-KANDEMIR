using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMainMenu : MonoBehaviour
{
    // Name of the main menu scene
    public string mainMenuScene = "MainMenu";

    void Update()
    {
        // Check if escape key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {   
            //Call method to load main menu
            LoadMainMenu();
        }
    }

    //Create method which loads the main menu scene
    void LoadMainMenu()
    {   
        //Load main menu
        SceneManager.LoadScene(mainMenuScene);
    }
}
