using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class RollButton : MonoBehaviour
{
    public InputField bet;
    public Slot slot;
    public Text prizeText;
    public NumberText balance;
    public Statistic statistic;

    private IEnumerator _rolling;
    private int _streak;
    private readonly int _startChips = 1000;
    
    public void Roll()
    {
        var betSize = int.Parse(bet.text);
        if(!TryMakeBet(betSize)) return;
        var multiplier = 0;
        var streak = 0;
        if (Distributions.Current(_streak))
        {
            multiplier = MultiplierCalculator.GetMultiplier();
            var prize = multiplier * betSize;
            prizeText.text = prize.ToString();
            balance.Value += prize;
            streak = _streak;
            _streak = 0;
        }
        statistic.RecordResult(streak, multiplier, balance.Value);
        
        slot.PushToChangeWinningLine(multiplier);
        slot.PushToChangeEmptyLines();
        slot.ChangeLines();
    }
    
    private bool TryMakeBet(int betSize)
    {
        if (betSize < 0 || balance.Value < betSize)
        {
            prizeText.text = "wrong bet size";
            return false;
        }

        balance.Value -= betSize;
        _streak++;
        return true;
    }
    
    public void Rerun()
    {
        balance.Value = _startChips;
        _streak = 0;
        statistic.Clear();
    }
    
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _rolling = Rolling();
            StartCoroutine(_rolling);
            return;
        }
        
        if (Input.GetKeyUp(KeyCode.Space))
        {
            StopCoroutine(_rolling);
        }
    }

    private IEnumerator Rolling()
    {
        Roll();
        yield return new WaitForSeconds(0.5f);
        while (Input.GetKey(KeyCode.Space))
        {
            Roll();
            yield return new WaitForSeconds(0.01f);
        }
    }
}
