using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GraphicPoint : MonoBehaviour
{
    public const float Side = 0.2f;
    [HideInInspector] public Transform pointTransform;

    public abstract void Clear();
}
