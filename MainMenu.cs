using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    //Name of next scenes to be opened
    string mechanicsScene = "PixelArena";
    string mapScene = "LowPolyFPS_Lite_Demo";

    public void StartNewGame()
    {   
        // Load the map scene normally
        SceneManager.LoadScene(mapScene);

        // Load the mechanics scene additively
        SceneManager.LoadScene(mechanicsScene, LoadSceneMode.Additive); 
    }

    public void ExitApplication()
    {   
        //Exit the game
        Application.Quit();
    }
}
