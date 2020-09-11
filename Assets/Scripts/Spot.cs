using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spot : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    //public Sprite[] roflanSprites;

    //private static readonly Symbol[] RoflanSymbols = {Symbol.J, Symbol.Q, Symbol.K};

    public void ChangeSymbol(Symbol symbol)
    {
        spriteRenderer.sprite = sprites[(int) symbol];
    }
}
