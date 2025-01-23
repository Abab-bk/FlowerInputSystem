using FlowerInputSystem.Actions;
using FlowerInputSystem.Values;
using VYaml.Annotations;

namespace FlowerInputSystem.Conditions;

[YamlObject]
public partial struct PressCondition() : IInputCondition
{
    public float Actuation { get; set; } = 1f;
    
    public InputActionState Evaluate(IActionValue actionValue, float delta) =>
        actionValue.IsActuated(Actuation) ?
            InputActionState.Fired : InputActionState.None;
}