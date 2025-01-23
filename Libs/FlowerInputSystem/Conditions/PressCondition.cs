using FlowerInputSystem.Actions;
using FlowerInputSystem.Values;

namespace FlowerInputSystem.Conditions;

public struct PressCondition() : IInputCondition
{
    public float Actuation { get; set; } = 1f;
    
    public InputActionState Evaluate(IActionValue actionValue, float delta) =>
        actionValue.IsActuated(Actuation) ?
            InputActionState.Fired : InputActionState.None;
}