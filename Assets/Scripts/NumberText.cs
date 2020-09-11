using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberText : MonoBehaviour
{
    public int Value
    {
        get => _value;
        set
        {
            _text.text = value.ToString();
            _value = value;
        }
    }
    
    private Text _text;
    private int _value;
    
    private void Start()
    {
        _text = GetComponent<Text>();
        int.TryParse(_text.text, out _value);
    }
}
