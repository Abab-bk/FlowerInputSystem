using FlowerInputSystem.Values;

namespace FlowerInputSystem.Actions;

internal struct TriggerTracer(IActionValue value)
{
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
    
    public void ApplyConditions()
    {
        // TODO
    }

    public InputActionState GetActionState()
    {
        if (Blocked) return InputActionState.None;

        if (!FoundExplicit && !FoundImplicit)
        {
            if (value.AsBool()) return InputActionState.Fired;
            return InputActionState.None;
        }

        if ((!FoundExplicit || AnyExplicitFired) &&
            AllImplicitsFired)
            return InputActionState.Fired;
        if (FoundActive) return InputActionState.Ongoing;
        return InputActionState.None;
    }
}