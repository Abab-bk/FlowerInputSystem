using FlowerInputSystem.Actions;
using FlowerInputSystem.Values;
using VYaml.Annotations;

namespace FlowerInputSystem.Conditions;

[YamlObject]
[YamlObjectUnion("!hold", typeof(HoldCondition))]
[YamlObjectUnion("!press", typeof(PressCondition))]
[YamlObjectUnion("!just-press", typeof(JustPressCondition))]
public partial interface IInputCondition
{
    public float Actuation { get; set; }
    public InputActionState Evaluate(IActionValue actionValue, float delta);
    public ConditionKind GetKind() => ConditionKind.Explicit;
}