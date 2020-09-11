using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graphic<T> : MonoBehaviour 
    where T: GraphicPoint
{
    public GameObject point;
    public List<T> points;
    public const int AxisLength = 54;

    private void Start()
    {
        points = new List<T>();
        var graphicTransform = transform;
        var position = graphicTransform.position;
        for (var i = 0; i < AxisLength; i++)
        {
            var newPoint = Instantiate(point, new Vector2(-2.75f + i * GraphicPoint.Side/2 + position.x, -1.15f + position.y + GraphicPoint.Side/2),
                Quaternion.identity, graphicTransform);
            var graphicPoint = newPoint.GetComponent<T>();
            points.Add(graphicPoint);
        }
    }

    public void Clear()
    {
        foreach (var p in points)
        {
            p.Clear();
        }
    }
}
