public class WaterGraphic: Graphic<WaterGraphicPoint>
{
    public void Increment(int i)
    {
        points[i].Increment();
    }
}