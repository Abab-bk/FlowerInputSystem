using FlowerInputSystem.Binds;
using FlowerInputSystem.Values;
using Godot;
using VYaml.Annotations;

namespace FlowerInputSystem.Actions;

[YamlObject]
public partial class InputAction(
    string name,
    List<InputBind> binds
    )
{
    public event Action OnFired = () => { };
    public event Action OnGoing = () => { };
    
    public string Name { get; set; } = name;
    public Accumulation Accumulation { get; set; }
    public ValueDimension ValueDimension { get; set; }
    public List<InputBind> Binds { get; set; } = binds;
    
    [YamlIgnore] public bool BoolValue => _axis3DActionValue.AsBool();
    [YamlIgnore] public float Axis1DValue => _axis3DActionValue.AsAxis1D();
    [YamlIgnore] public Vector2 Axis2DValue => _axis3DActionValue.AsAxis2D();
    [YamlIgnore] public Vector3 Axis3DValue => _axis3DActionValue.AsAxis3D();

    public bool RequireReset;
    
    private Axis3DActionValue _axis3DActionValue;

    public void Update(float delta)
    {
        var tracer = new TriggerTracer(IActionValue.Zero(ValueDimension));
        
        foreach (var bind in Binds)
        {
            var value = InputSystem.Reader.GetValue(bind.Input);

            if (RequireReset && bind.FirstActivation)
            {
                if (value.AsBool()) continue;
                bind.FirstActivation = false;
            }

            var currentTracer = new TriggerTracer(value);
            currentTracer.ApplyConditions(delta, bind.Conditions);

            if (currentTracer.GetActionState() == InputActionState.None) continue;
            
            tracer.Combine(ref currentTracer, Accumulation);
        }
        
        _axis3DActionValue = new Axis3DActionValue
        {
            Value = tracer.Value.AsAxis3D()
        };

        var state = tracer.GetActionState();
        switch (state)
        {
            case InputActionState.None: break;
            case InputActionState.Ongoing: OnGoing(); break;
            case InputActionState.Fired: OnFired(); break;
            default: throw new ArgumentOutOfRangeException();
        }

        // if (state != InputActionState.None)
        // {
        //     foreach (var input in _consumeBuffer)
        //     {
        //         InputSystem.Reader.Consume(input);
        //     }
        // }
        //
        // _consumeBuffer.Clear();
    }
}