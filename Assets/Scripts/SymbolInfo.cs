using System.Collections.Generic;
using System.Diagnostics.SymbolStore;

public static class SymbolInfo
{
    public static readonly Symbol[] Symbols = {Symbol.A, Symbol.K, Symbol.Q, Symbol.J, Symbol.Ten,};
    public const int SymbolsCount = 5;

    public static readonly Dictionary<Symbol, int> Payout = new Dictionary<Symbol, int>
    {
        [Symbol.A] = 20,
        [Symbol.K] = 10,
        [Symbol.Q] = 5,
        [Symbol.J] = 2,
        [Symbol.Ten] = 1,
    };
}