﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChange : MonoBehaviour
{
    public static string SceneName;
    public static int Level;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GoLoadingScene(string name)
    {
        SceneName = name;
        SceneManager.LoadScene("LoadScene");
    }
    public void ChooseLevel(int level)
    {
        Level = level;
    }
}
