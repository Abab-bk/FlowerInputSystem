using FlowerInputSystem.Binds;
using FlowerInputSystem.Conditions;
using FlowerInputSystem.Modifiers;
using FlowerInputSystem.Values;

namespace FlowerInputSystem.Actions;

public class InputAction(
    string name,
    List<InputBind> binds,
    List<IInputModifier>? modifiers = null,
    List<IInputCondition>? conditions = null
    )
{
    public event Action OnFired = () => { };
    
    public string Name { get; set; } = name;
    public List<InputBind> Binds { get; set; } = binds;
    public List<IInputModifier> Modifiers { get; set; } = modifiers ?? new();
    public List<IInputCondition> Conditions { get; set; } = conditions ?? new();
    public ValueDimension ValueDimension { get; set; }

    public void Update()
    {
        var tracer = new TriggerTracer(IActionValue.Zero(ValueDimension));

        var fired = false;
        
        foreach (var bind in Binds)
        {
            var value = InputSystem.Reader.GetValue(bind.Input);
            if (value.AsBool())
            {
                fired = true;
                break;
            }
            // var currentTracer = new TriggerTracer(value);
            // currentTracer.ApplyModifiers();
            // currentTracer.ApplyConditions();
            //
            // var currentState = currentTracer.GetActionState();
            // if (currentState == InputActionState.None) continue;
            //
            // var tracerState = tracer.GetActionState();
            // if (currentState == tracerState)
            // {
            // }
            // TODO
        }
        
        // tracer.ApplyModifiers();
        // tracer.ApplyConditions();

        if (fired) OnFired();
    }
}