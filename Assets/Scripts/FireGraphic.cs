using UnityEngine;

public class FireGraphic: Graphic<FireGraphicPoint>
{
    public void AddPoint(int value)
    {
        RightShift();
        points[0].SetValue(value);
    }

    private void RightShift()
    {
        for (var i = points.Count - 1; i > 0; i--)
        {
            var position = new Vector2(points[i].pointTransform.position.x, points[i - 1].pointTransform.position.y);
            points[i].pointTransform.position = position;
            points[i].pointTransform.localScale = points[i - 1].pointTransform.localScale;
        }
    }
}