using System;
using System.Collections.Generic;
using Unity.Collections;
using Random = UnityEngine.Random;

public static class Distributions
{
    private static double _remainPercent = 1;
    
    private const int NormalMode = 10;
    private const int NormalVariance = 4;
    private static readonly Func<int, double> NormalProbabilityDensity =
        x => Math.Pow(Math.E, -Math.Pow(x - NormalMode, 2) / (2 * Math.Pow(NormalVariance, 2))) 
             / (NormalVariance * Math.Sqrt(2 * Math.PI));
    
    private const int ToSkip = 5;
    private const int IntervalLength = 9;
    
    public static readonly Dictionary<string, Func<int, bool>> Dictionary = new Dictionary<string, Func<int, bool>>
    {
        ["Normal"] = x =>
        {
            if (x > 70)
            {
                _remainPercent = 1;
                return true;
            }
            var currentPercent = NormalProbabilityDensity(x);
            var chance = currentPercent / _remainPercent;
            var hasWon = chance >= Random.Range(0, 1f);
            _remainPercent = hasWon ? 1 : _remainPercent - currentPercent;
            return hasWon;
        },
        ["Exponential"] = x => 1 > Random.Range(0, 10),
        ["Interval"] = x => x > ToSkip && 1.0 / (IntervalLength - (x - ToSkip - 1)) > Random.Range(0, 1f),
    };

    public static Func<int, bool> Current = Dictionary["Normal"];
}