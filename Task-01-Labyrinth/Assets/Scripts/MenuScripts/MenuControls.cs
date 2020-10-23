using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuControls : MonoBehaviour
{
    public void LaunchFirstLevel()
    {
        Debug.Log("Launching first level...");
        LaunchGame.LaunchLevel(GameSettings.DefaultStartScene);
    }

    public void Close()
    {
        Debug.Log("Closing game...");
        Application.Quit();
    }
}
