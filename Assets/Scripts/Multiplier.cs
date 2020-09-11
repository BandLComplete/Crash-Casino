using UnityEngine;

public class Multiplier
{
    private readonly float _chance;
    private readonly int _minWin;
    private readonly int _maxWin;
    
    public Multiplier(float chance, int minWin, int maxWin)
    {
        _chance = chance;
        _minWin = minWin;
        _maxWin = maxWin + 1;
    }

    public bool TryWin(ref float roll, out int multiplier)
    {
        multiplier = 0;
        if (_chance < roll)
        {
            roll -= _chance;
            return false;
        }
        multiplier = Random.Range(_minWin, _maxWin);
        return true;
    }
}