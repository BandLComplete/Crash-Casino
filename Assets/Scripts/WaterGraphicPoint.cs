using System.Collections.Generic;
using UnityEngine;

public class WaterGraphicPoint: GraphicPoint
{
    public GameObject drop;
    public List<GameObject> drops = new List<GameObject>();

    public void Start()
    {
        pointTransform = GetComponent<Transform>();
    }

    public void Increment()
    {
        var position = pointTransform.position;
        var newDrop = Instantiate(drop, new Vector2(position.x, position.y + 2.5f), Quaternion.identity);
        drops.Add(newDrop);
    }

    public override void Clear()
    {
        foreach (var d in drops)
        {
            Destroy(d);
        }
        drops.Clear();
    }
}