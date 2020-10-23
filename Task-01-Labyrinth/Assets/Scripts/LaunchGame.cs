using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using System.IO;

public static class GameSettings
{
    //public static string DefaultStartScene;
    //public static string CurrentScene;
    //public static int NumLevels;
    //public static List<string> Levels;

    public static int DefaultStartScene;
    public static int CurrentScene;
    public static int NumLevels;

    static GameSettings()
    {
        //DefaultStartScene = "Labyrinth-01";
        //Levels = GetLevels();
        //NumLevels = Levels.Count;

        DefaultStartScene = 1;
        NumLevels = 5; //TODO: any way to automate??
    }

    private static List<string> GetLevels()
    {
        List<string> levels = new List<string>();
#if UNITY_EDITOR
        foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
        {
            if (scene.enabled && scene.path.Contains("Labyrinth-"))
            {
                Debug.Log(string.Format("\t{0} detected...",scene.path));
                string[] pathComps = scene.path.Replace("\\","/").Split('/');
                levels.Add(pathComps[pathComps.Length-1].Split('.')[0].Trim());
            }
        }
#endif

#if UNITY_WEBGL || UNITY_STANDALONE 
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            if (scene.name == SceneManager.GetActiveScene().name && scene.path.Contains("Labyrinth-"))
            {
                Debug.Log(string.Format("\t{0} detected...", scene.path));
                string[] pathComps = scene.path.Replace("\\", "/").Split('/');
                levels.Add(pathComps[pathComps.Length - 1].Split('.')[0].Trim());
            }
        }
#endif
        Debug.Log(string.Format("{0} level(s) were discovered.", levels.Count));
        return levels;
    }

    public static bool LevelExists(int levelNum)
    {
        return levelNum <= NumLevels;
    }
}

public static class LaunchGame
{

    public static void LaunchLevel(int scene)
    {
        Debug.Log(string.Format("Launching scene {0}...", scene));
        PlayerController.PlayerName = PlayerController.PlayerNameDefault;
        SceneManager.LoadScene(scene);
    }

    public static void RestartLevel()
    {
        Debug.Log(string.Format("Restarting level {0}...", SceneManager.GetActiveScene().name));
        LaunchLevel(SceneManager.GetActiveScene().buildIndex);
    }
    
    public static void ReturnToMenu()
    {
        Debug.Log("Returning to main menu...");
        LaunchLevel(0);
    }
}
