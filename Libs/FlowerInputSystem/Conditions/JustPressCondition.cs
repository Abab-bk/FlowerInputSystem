using FlowerInputSystem.Actions;
using FlowerInputSystem.Values;

namespace FlowerInputSystem.Conditions;

public struct JustPressCondition() : IInputCondition
{
    public float Actuation { get; set; } = 1f;
    private bool _actuated;
    
    public InputActionState Evaluate(IActionValue actionValue, float delta)
    {
        var previouslyActuated = _actuated;
        _actuated = actionValue.IsActuated(Actuation);
        return _actuated && !previouslyActuated ?
            InputActionState.Fired : InputActionState.None;
    }
}