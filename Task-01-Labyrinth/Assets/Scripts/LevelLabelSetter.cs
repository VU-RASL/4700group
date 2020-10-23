using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelLabelSetter : MonoBehaviour
{
    private void Start()
    {
        string activeSceneName = SceneManager.GetActiveScene().name;
        GameObject.Find("Canvas/LevelLabel").GetComponent<Text>().text = activeSceneName;
    }
}
