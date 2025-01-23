using System.Numerics;

namespace FlowerInputSystem.Values;

public static class ActionValueExtension
{
    public static Vector3 AsAxis3D(this IActionValue actionValue)
    {
        return actionValue switch
        {
            BoolActionValue { Value: true } => Vector3.UnitX,
            BoolActionValue boolActionValue => Vector3.Zero,
            Axis1DActionValue axis1DActionValue => Vector3.UnitX * axis1DActionValue.Value,
            Axis2DActionValue axis2DActionValue => new Vector3(axis2DActionValue.Value, 0f),
            Axis3DActionValue axis3DActionValue => axis3DActionValue.Value,
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public static float AsAxis1D(this IActionValue actionValue)
    {
        return actionValue switch
        {
            BoolActionValue { Value: true } => 1f,
            BoolActionValue boolActionValue => 0f,
            Axis1DActionValue axis1DActionValue => axis1DActionValue.Value,
            Axis2DActionValue axis2DActionValue => axis2DActionValue.Value.X,
            Axis3DActionValue axis3DActionValue => axis3DActionValue.Value.X,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
    
    public static Vector2 AsAxis2D(this IActionValue actionValue)
    {
        return actionValue switch
        {
            BoolActionValue { Value: true } => Vector2.UnitX,
            BoolActionValue boolActionValue => Vector2.Zero,
            Axis1DActionValue axis1DActionValue => Vector2.UnitX * axis1DActionValue.Value,
            Axis2DActionValue axis2DActionValue => axis2DActionValue.Value,
            Axis3DActionValue axis3DActionValue => new Vector2(axis3DActionValue.Value.X, axis3DActionValue.Value.Y),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
    
    public static bool AsBool(this IActionValue actionValue) =>
        actionValue switch
        {
            BoolActionValue boolActionValue => boolActionValue.Value,
            Axis1DActionValue axis1DActionValue => axis1DActionValue.Value != 0f,
            Axis2DActionValue axis2DActionValue => axis2DActionValue.Value != Vector2.Zero,
            Axis3DActionValue axis3DActionValue => axis3DActionValue.Value != Vector3.Zero,
            _ => throw new ArgumentOutOfRangeException()
        };
    
    public static Axis3DActionValue Convert(
        this IActionValue actionValue,
        ValueDimension valueDimension
        ) =>
        valueDimension switch
        {
            ValueDimension.Bool => new Axis3DActionValue
            {
                Value = new BoolActionValue(actionValue.AsBool()).AsAxis3D()
            },
            ValueDimension.Axis1D => new Axis3DActionValue
            {
                Value = new Axis1DActionValue
                {
                    Value = actionValue.AsAxis1D()
                }.AsAxis3D()
            },
            ValueDimension.Axis2D => new Axis3DActionValue
            {
                Value = new Axis2DActionValue
                {
                    Value = actionValue.AsAxis2D()
                }.AsAxis3D()
            },
            ValueDimension.Axis3D => new Axis3DActionValue
            {
                Value = actionValue.AsAxis3D()
            },
            _ => throw new ArgumentOutOfRangeException(nameof(valueDimension), valueDimension, null)
        };
}