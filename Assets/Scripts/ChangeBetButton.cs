using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChangeBetButton : MonoBehaviour
{
    public NumberText balance;
    public InputField bet;
    public bool increment;
    
    [HideInInspector] 
    public int step = 1;

    private int _callsCount;
    private readonly Timer _timer = new Timer(500);

    public void ChangeBet()
    {
        _timer.Stop();
        _timer.Start();
        _callsCount++;
        step = Math.Max(_callsCount / 5 * 5, 1);
        var currentValue = int.Parse(bet.text);
        currentValue += (increment ? 1 : -1) * step;
        if (currentValue <= 0) currentValue = 1;
        if (balance.Value < currentValue) currentValue = balance.Value;
        bet.text = currentValue.ToString();
    }

    private void ResetTimer(object source, ElapsedEventArgs e)
    {
        _callsCount = 0;
        step = 1;
        _timer.Stop();
    }

    private void Start()
    {
        _timer.Elapsed += ResetTimer;
    }
}
