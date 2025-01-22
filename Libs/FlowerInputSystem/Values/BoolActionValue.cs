namespace FlowerInputSystem.Values;

public struct BoolActionValue(bool value) : IActionValue
{
    public bool Value { get; set; } = value;
    public bool AsBool() => Value;
}