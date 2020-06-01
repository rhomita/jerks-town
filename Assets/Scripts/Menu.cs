﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void Play() 
    {
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
