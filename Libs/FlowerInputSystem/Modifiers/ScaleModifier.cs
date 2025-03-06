using FlowerInputSystem.Values;
using Godot;
using VYaml.Annotations;

namespace FlowerInputSystem.Modifiers;

[YamlObject]
public partial struct ScaleModifier : IInputModifier
{
    public Vector3 Factor { get; set; }
    
    public IActionValue Apply(IActionValue value)
    {
        switch (value)
        {
            case BoolActionValue boolActionValue:
            {
                var v = boolActionValue.Value ? 1f : 0f;
                return new Axis1DActionValue
                {
                    Value = v * Factor.X
                };
            }
            case Axis1DActionValue axis1DActionValue:
                return new Axis1DActionValue
                {
                    Value = axis1DActionValue.Value * Factor.X
                };
            case Axis2DActionValue axis2DActionValue:
                return new Axis2DActionValue
                {
                    Value = new Vector2(
                        axis2DActionValue.Value.X * Factor.X,
                        axis2DActionValue.Value.Y * Factor.Y
                    )
                };
            case Axis3DActionValue axis3DActionValue:
                return new Axis3DActionValue
                {
                    Value = new Vector3(
                        axis3DActionValue.Value.X * Factor.X,
                        axis3DActionValue.Value.Y * Factor.Y,
                        axis3DActionValue.Value.Z * Factor.Z
                    )
                };
            default:
                throw new Exception("Unsupported value type");
        }
    }
}