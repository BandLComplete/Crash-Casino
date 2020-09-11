using UnityEngine;

public static class MultiplierCalculator
{
    private static readonly Multiplier[] Multipliers =
    {
        new Multiplier(0.3f, 1,3),
        new Multiplier(0.65f, 5,15),
        new Multiplier(0.05f, 20,60),
    };
    public static int GetMultiplier()
    {
        var multiplier = 0;
        var seed = Random.Range(0, 1f);
        foreach (var m in Multipliers)
        {
            if(m.TryWin(ref seed, out multiplier))
                break;
        }
        return multiplier;
    }
}