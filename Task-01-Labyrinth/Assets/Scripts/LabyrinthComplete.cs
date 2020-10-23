using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabyrinthComplete : MonoBehaviour
{
    public static bool isComplete = false;
    private static int currLevel = 1;

    public void Completed()
    {
        isComplete = true;
        StartCoroutine(BeginNextScene());
    }

    private IEnumerator BeginNextScene()
    {
        yield return new WaitForSeconds(2);
        isComplete = false;
        int nextLevel = ++currLevel;
        if (GameSettings.LevelExists(nextLevel))
            LaunchGame.LaunchLevel(nextLevel);
        else
        {
            currLevel = 1;
            LaunchGame.ReturnToMenu();
        }
    }
}
