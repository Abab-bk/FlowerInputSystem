using FlowerInputSystem.Conditions;
using FlowerInputSystem.Inputs;
using FlowerInputSystem.Modifiers;
using VYaml.Annotations;

namespace FlowerInputSystem.Binds;

[YamlObject]
public partial struct InputBind()
{
    public IInput Input { get; set; }
    public IEnumerable<IInputModifier> Modifiers { get; set; } = [];
    public IEnumerable<IInputCondition> Conditions { get; set; } = [];
}