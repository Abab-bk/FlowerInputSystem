using FlowerInputSystem.Conditions;
using FlowerInputSystem.Inputs;
using FlowerInputSystem.Modifiers;

namespace FlowerInputSystem.Binds;

public struct InputBind
{
    public IInput Input { get; set; }
    public IEnumerable<IInputModifier> Modifiers { get; set; }
    public IEnumerable<IInputCondition> Conditions { get; set; }
}