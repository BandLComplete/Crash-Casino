using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Profile : MonoBehaviour
{
    public GameObject[] enterObjects;
    public GameObject[] profileObjects;
    public Text user;
    public Text mark;
    public LevelButton[] levelButtons;
    
    public void Load()
    {
        ChangeActivePanel();
        CalculateMark();
        user.text = UserController.UserName;
        mark.text = "Mark: " + UserController.UserMark;
        LoadLevelMarks();
    }

    public void Exit()
    {
        UserController.UserName = string.Empty;
        UserController.UserMark = string.Empty;
        UserController.LevelMarks = Marks.StartMarks();
        LoadLevelMarks();
        ChangeActivePanel();
    }

    private void CalculateMark()
    {
        var sum = UserController.LevelMarks.Values.Aggregate(0.0, (current, m) => current + m);
        var average = Math.Round(sum / UserController.LevelMarks.Count, 1);
        UserController.UserMark = average.ToString();
    }

    public void LoadLevelMarks()
    {
        foreach (var button in levelButtons)
        {
            button.levelMark.text = UserController.LevelMarks[button.levelName.text].ToString();
        }
    }

    private void ChangeActivePanel()
    {
        foreach (var obj in profileObjects.Concat(enterObjects))
        {
            obj.SetActive(!obj.activeSelf);
        }
    }

    private void Start()
    {
        foreach (var obj in enterObjects)
        {
            obj.SetActive(true);
        }
        
        foreach (var obj in profileObjects)
        {
            obj.SetActive(false);
        }
    }
}
