using Vector2 = Godot.Vector2;

namespace FlowerInputSystem.Values;

public struct Axis2DActionValue : IActionValue
{
    public Vector2 Value { get; set; }

    public ValueDimension Dimension { get; }

    public bool IsActuated(float actuation)
    {
        throw new NotImplementedException();
    }
}