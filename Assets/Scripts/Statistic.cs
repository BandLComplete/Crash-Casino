using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statistic : MonoBehaviour
{
    public FireGraphic lastMultipliers;
    public WaterGraphic spinsToWin;
    
    public void RecordResult(int streak, int multiplier, int balance, int startChips = 1000)
    {
        lastMultipliers.AddPoint(multiplier);
        if (multiplier > 0)
        {
            AddStreakToGraphic(streak);
            RecordMark(balance, startChips);
        }
    }
    
    private void AddStreakToGraphic(int streak)
    {
        if (streak >= Graphic<GraphicPoint>.AxisLength) return;
        streak--;
        spinsToWin.Increment(streak);
    }
    
    private void RecordMark(int balance, int startChips)
    {
        if (balance > startChips)
        {
            var mark = (int)Math.Ceiling(Math.Min((double)balance / startChips - 1, 1) * 100);
            if (mark > UserController.LevelMarks[Slot.LevelName])
            {
                UserController.LevelMarks[Slot.LevelName] = mark;
                StartCoroutine(UserController.WriteMark(Slot.LevelName, mark));
            }
        }
    }

    public void Clear()
    {
        lastMultipliers.Clear();
        spinsToWin.Clear();
    }
}
