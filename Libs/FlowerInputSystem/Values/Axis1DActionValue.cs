namespace FlowerInputSystem.Values;

public struct Axis1DActionValue : IActionValue
{
    public float Value { get; set; }

    public ValueDimension Dimension { get; }

    public bool IsActuated(float actuation) => Value > actuation;
}