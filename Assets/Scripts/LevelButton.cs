using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    public Text levelName;
    public Text levelMark;

    public void LoadScene()
    {
        var level = levelName.text;
        Distributions.Current = Distributions.Dictionary[level];
        Slot.LevelName = level;
        SceneManager.LoadScene("Level");
    }
}
