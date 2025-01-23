using FlowerInputSystem.Binds;
using FlowerInputSystem.Conditions;
using FlowerInputSystem.Inputs;
using FlowerInputSystem.Modifiers;
using FlowerInputSystem.Values;
using Godot;
using VYaml.Annotations;

namespace FlowerInputSystem.Actions;

[YamlObject]
public partial class InputAction(
    string name,
    List<InputBind> binds,
    List<IInputModifier>? modifiers = null,
    List<IInputCondition>? conditions = null)
{
    public event Action OnFired = () => { };
    public event Action OnGoing = () => { };
    
    public string Name { get; set; } = name;
    public Accumulation Accumulation { get; set; }
    public ValueDimension ValueDimension { get; set; }
    public List<InputBind> Binds { get; set; } = binds;
    public List<IInputModifier> Modifiers { get; set; } = modifiers ?? new();
    public List<IInputCondition> Conditions { get; set; } = conditions ?? new();

    [YamlIgnore] public bool BoolValue => _axis3DActionValue.AsBool();
    [YamlIgnore] public float Axis1DValue => _axis3DActionValue.AsAxis1D();
    [YamlIgnore] public Vector2 Axis2DValue => _axis3DActionValue.AsAxis2D();
    [YamlIgnore] public Vector3 Axis3DValue => _axis3DActionValue.AsAxis3D();
    
    private Axis3DActionValue _axis3DActionValue = new();
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
        _axis3DActionValue = new Axis3DActionValue()
        {
            Value = tracer.Value.AsAxis3D()
        };
        if (tracer.AnyExplicitFired) OnFired();
    }
}