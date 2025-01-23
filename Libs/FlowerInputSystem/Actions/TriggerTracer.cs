using FlowerInputSystem.Conditions;
using FlowerInputSystem.Values;
using Godot;

namespace FlowerInputSystem.Actions;

internal struct TriggerTracer(IActionValue value)
{
    public IActionValue Value { get; set; } = value;
    public bool FoundExplicit { get; set; }
    public bool AnyExplicitFired { get; set; }
    public bool FoundActive { get; set; }
    public bool FoundImplicit { get; set; }
    public bool AllImplicitsFired { get; set; }
    public bool Blocked { get; set; }
    
    public void ApplyModifiers()
    {
        // TODO
    }
    
    public void ApplyConditions(
        float delta,
        IEnumerable<IInputCondition> conditions
        )
    {
        foreach (var inputCondition in conditions)
        {
            var state = inputCondition.Evaluate(Value, delta);
            switch (inputCondition.GetKind())
            {
                case ConditionKind.Explicit:
                    FoundExplicit = true;
                    AnyExplicitFired |= state == InputActionState.Fired;
                    FoundActive |= state != InputActionState.None;
                    break;
                case ConditionKind.Implicit:
                    FoundImplicit = true;
                    AllImplicitsFired &= state == InputActionState.Fired;
                    FoundActive |= state != InputActionState.None;
                    break;
                case ConditionKind.Blocker:
                    Blocked = state == InputActionState.Fired;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    public InputActionState GetActionState()
    {
        if (Blocked) return InputActionState.None;

        return FoundExplicit switch
        {
            false when !FoundImplicit =>
                Value.AsBool() ? InputActionState.Fired : InputActionState.None,
            false or true when AllImplicitsFired => InputActionState.Fired,
            _ => FoundActive ? InputActionState.Ongoing : InputActionState.None
        };
    }

    public void Combine(TriggerTracer other, Accumulation accumulation)
    {
        Vector3 accumulated;

        switch (accumulation)
        {
            case Accumulation.Cumulative:
                accumulated = Value.AsAxis3D() + other.Value.AsAxis3D();
                break;
            case Accumulation.MaxAbs:
                var selfValue = Value.AsAxis3D();
                var otherValue = other.Value.AsAxis3D();
                accumulated = new Vector3(
                    Math.Abs(selfValue.X) < Math.Abs(otherValue.X) ? otherValue.X : selfValue.X,
                    Math.Abs(selfValue.Y) < Math.Abs(otherValue.Y) ? otherValue.Y : selfValue.Y,
                    Math.Abs(selfValue.Z) < Math.Abs(otherValue.Z) ? otherValue.Z : selfValue.Z
                );
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(accumulation), accumulation, null);
        }
        
        var dimension = Value.Dimension;
        Value = new Axis3DActionValue
        {
            Value = accumulated
        }.Convert(dimension);
        FoundExplicit |= other.FoundExplicit;
        AnyExplicitFired |= other.AnyExplicitFired;
        FoundActive |= other.FoundActive;
        FoundImplicit |= other.FoundImplicit;
        AllImplicitsFired &= other.AllImplicitsFired;
        Blocked |= other.Blocked;
    }
}