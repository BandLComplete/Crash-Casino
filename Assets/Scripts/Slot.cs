using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public static string LevelName = "Normal";
    
    public Spot[] spots;
    
    private readonly Symbol[][] _toChange = new Symbol[LinesCount][];
    private readonly bool[] _linesAvailability = new bool[LinesCount];
    private readonly Dictionary<Symbol, int> _countLinesOfSymbols = new Dictionary<Symbol, int>();
    private const int LinesCount = 3;
    private const int LineLength = 3;
    private readonly Spot[][] _lines = new Spot[LinesCount][];
    
    void Start()
    {
        foreach (var symbol in SymbolInfo.Symbols)
        {
            _countLinesOfSymbols[symbol] = 0;
        }
        
        for (var i = 0; i < LinesCount; i++)
        {
            _lines[i] = new Spot[LineLength];
            _toChange[i] = new Symbol[LineLength];
            _linesAvailability[i] = true;
            for (var j = 0; j < LineLength; j++)
            {
                _lines[i][j] = spots[j * LinesCount + i];
            }
        }
    }

    public void PushToChangeWinningLine(int multiplier)
    {
        foreach (var symbol in SymbolInfo.Symbols)
        {
            _countLinesOfSymbols[symbol] = multiplier / SymbolInfo.Payout[symbol];
            multiplier %= SymbolInfo.Payout[symbol];
        }

        foreach (var countLinesOfSymbol in _countLinesOfSymbols.Where(s => s.Value > 0))
        {
            var count = countLinesOfSymbol.Value;
            while (count > 0)
            {
                var lineNumber = GetAvailableRandomLineNumber();
                if(lineNumber == -1) break;
                for (var i = 0; i < LineLength; i++)
                {
                    _toChange[lineNumber][i] = countLinesOfSymbol.Key;
                }

                _linesAvailability[lineNumber] = false;
                count--;
            }
        }
    }
    
    public void PushToChangeEmptyLines()
    {
        var emptyLineNumber = GetAvailableRandomLineNumber();
        while (emptyLineNumber != -1)
        {
            for (var i = 0; i < LineLength; i++)
            {
                var randomSymbol = (Symbol)Random.Range(0, SymbolInfo.SymbolsCount);
                while (i == 2 && 
                       randomSymbol == _toChange[emptyLineNumber][i - 1] && randomSymbol == _toChange[emptyLineNumber][i - 2])
                {
                    randomSymbol = (Symbol)Random.Range(0, SymbolInfo.SymbolsCount);
                }

                _toChange[emptyLineNumber][i] = randomSymbol;
                _linesAvailability[emptyLineNumber] = false;
            }
            
            emptyLineNumber = GetAvailableRandomLineNumber();
        }
    }
    
    private int GetAvailableRandomLineNumber()
    {
        if (_linesAvailability.All(status => !status))
            return -1;
        var lineNumber = Random.Range(0, LinesCount);
        while(!_linesAvailability[lineNumber])
            lineNumber = Random.Range(0, LinesCount);
        return lineNumber;
    }

    public void ChangeLines()
    {
        for (var i = 0; i < LinesCount; i++)
        {
            _linesAvailability[i] = true;
            for (var j = 0; j < LineLength; j++)
                _lines[i][j].ChangeSymbol(_toChange[i][j]);
        }
    }
}
