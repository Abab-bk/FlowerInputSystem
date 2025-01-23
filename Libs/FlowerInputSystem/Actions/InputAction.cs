using FlowerInputSystem.Binds;
using FlowerInputSystem.Conditions;
using FlowerInputSystem.Inputs;
using FlowerInputSystem.Modifiers;
using FlowerInputSystem.Values;
using Godot;

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
    public Accumulation Accumulation { get; set; }
    public ValueDimension ValueDimension { get; set; }
    public List<InputBind> Binds { get; set; } = binds;
    public List<IInputModifier> Modifiers { get; set; } = modifiers ?? new();
    public List<IInputCondition> Conditions { get; set; } = conditions ?? new();
    
    private readonly List<IInput> _consumeBuffer = new();

    public void Update(float delta)
    {
        var tracer = new TriggerTracer(IActionValue.Zero(ValueDimension));
        
        foreach (var bind in Binds)
        {
            var value = InputSystem.Reader.GetValue(bind.Input);
            
            var currentTracer = new TriggerTracer(value);
            currentTracer.ApplyConditions(delta, bind.Conditions);
            
            tracer.Combine(currentTracer, Accumulation);
        }
        
        tracer.ApplyConditions(delta, Conditions);
        if (tracer.AnyExplicitFired) OnFired();
    }
}