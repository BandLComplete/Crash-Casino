using UnityEngine;

public class FireGraphicPoint: GraphicPoint
{
    private float _axis;
    private const int VerticalScaling = 2;

    public void SetValue(int value)
    {
        value = 1 + value / VerticalScaling;
        var position = new Vector2(pointTransform.position.x, _axis + value * Side / 2);
        var scaling = new Vector2(Side, value * Side);
        pointTransform.position = position;
        pointTransform.localScale = scaling;
    }
    
    public void Start()
    {
        pointTransform = GetComponent<Transform>();
        _axis = pointTransform.position.y - Side/2;
    }

    public override void Clear()
    {
        SetValue(0);
    }
    
}