using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThemeLoader : MonoBehaviour
{
    private string m_selectedTheme;
    private List<string> m_themes;
    private System.Random m_themeRandom;
    private Texture m_background;
    private Texture m_floor;
    private Texture m_wall;
    private Texture m_player;
    private AudioSource m_audioScrc;
    private AudioClip m_themeMusic;
    
    void Start()
    {
        //get listing of loaded themes
        this.GetThemes();

        this.SetRandomTheme();
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
            this.SetRandomTheme();
    }

    private void SetRandomTheme()
    {
        //select theme at random
        m_themeRandom = new System.Random(DateTime.Now.Millisecond);
        int randIndex = m_themeRandom.Next(m_themes.Count);
        this.LoadTheme(m_themes[randIndex]);
    }

    private void GetThemes()
    {
        m_themes = new List<string>();

        string themePath = Application.dataPath + "/Resources/Themes";

        //temporarily hardcode theme names until you think of a way to load them dynamically in a build environment
        m_themes.Add("Theme-Basketball");
        m_themes.Add("Theme-Soccer");
        m_themes.Add("Theme-Space");
        m_themes.Add("Theme-Spongebob");
        m_themes.Add("Theme-Vaporwave");
    }

    public void LoadTheme(string _theme_)
    {
        string path = "Themes/" + _theme_;

        m_background = (Texture)Resources.Load(path + "/background") as Texture;
        m_floor = (Texture)Resources.Load(path + "/floor") as Texture;
        m_wall = (Texture)Resources.Load(path + "/wall") as Texture;
        m_player = (Texture)Resources.Load(path + "/player") as Texture;

        m_themeMusic = (AudioClip)Resources.Load(path + "/audio") as AudioClip;

        this.ApplyTheme();
    }

    private void ApplyTheme()
    {
        this.ApplyThemeWalls();
        this.ApplyThemeBackground();
        this.ApplyThemeFloor();
        this.ApplyThemePlayer();
        this.ActivateThemeMusic();
    }

    private void ApplyThemeFloor()
    {
        GameObject.Find("LabyrinthObjects/Floor").GetComponent<MeshRenderer>().material.SetTexture("_MainTex", m_floor);
    }

    private void ApplyThemeBackground()
    {
        GameObject.Find("LabyrinthObjects/Background").GetComponent<MeshRenderer>().material.SetTexture("_MainTex", m_background);
    }

    private void ApplyThemeWalls()
    {
        foreach(Transform child in GameObject.Find("LabyrinthObjects/Walls").transform) {
            child.gameObject.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", m_wall);
        }
    }

    private void ApplyThemePlayer()
    {
        //TODO: "Player-Default" below is hardcoded; make dynamic
        GameObject.Find("PlayerObjects/Player-Default").GetComponent<MeshRenderer>().material.SetTexture("_MainTex", m_player);
    }

    private void ActivateThemeMusic()
    {
        GameObject.Find("Main Camera").AddComponent<AudioSource>();
        m_audioScrc = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        m_audioScrc.clip = m_themeMusic;
        m_audioScrc.loop = true;
        m_audioScrc.Play();
    }
}
