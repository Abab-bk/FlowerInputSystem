namespace FlowerInputSystem.Values;

public struct BoolActionValue(bool value) : IActionValue
{
    public bool Value { get; set; } = value;
    public ValueDimension Dimension { get; }
    public bool IsActuated(float actuation) => Value;
}