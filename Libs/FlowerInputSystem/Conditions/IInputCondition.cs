using FlowerInputSystem.Actions;
using FlowerInputSystem.Values;

namespace FlowerInputSystem.Conditions;

public interface IInputCondition
{
    public InputActionState Evaluate(IActionValue actionValue, float delta);
    public ConditionKind GetKind() => ConditionKind.Explicit;
}