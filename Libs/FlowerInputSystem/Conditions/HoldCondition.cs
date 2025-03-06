using FlowerInputSystem.Actions;
using FlowerInputSystem.Values;
using Godot;
using VYaml.Annotations;

namespace FlowerInputSystem.Conditions;

[YamlObject]
public partial struct HoldCondition(float duration) : IInputCondition
{
    [YamlIgnore] public float Elapsed { get; set; }
    
    public float Actuation { get; set; }
    public float Duration { get; set; } = duration;
    public bool IsOneShot { get; set; } = true;
    
    private bool _didShoot;
    
    public InputActionState Evaluate(IActionValue actionValue, float delta)
    {
        if (actionValue.IsActuated(Actuation))
        {
            Elapsed += delta;
            if (!(Elapsed >= Duration)) return InputActionState.Ongoing;
            if (IsOneShot && _didShoot) return InputActionState.None;
            _didShoot = true;
            return InputActionState.Fired;
        }
        
        Elapsed = 0f;
        _didShoot = false;
        return InputActionState.None;
    }
}