namespace FlowerInputSystem.Values;

public interface IActionValue
{
    public bool AsBool();

    public static IActionValue Zero(ValueDimension valueDimension)
    {
        switch (valueDimension)
        {
            case ValueDimension.Bool:
                return new BoolActionValue(false);
            case ValueDimension.Axis1D:
                return new Axis1DActionValue();
            case ValueDimension.Axis2D:
                return new Axis2DActionValue();
            case ValueDimension.Axis3D:
                return new Axis3DActionValue();
            default:
                throw new ArgumentOutOfRangeException(nameof(valueDimension), valueDimension, null);
        }
    }
}