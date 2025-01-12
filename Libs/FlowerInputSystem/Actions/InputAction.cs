using System.Runtime.Serialization;
using FlowerInputSystem.InputTriggers;
using Godot;

namespace FlowerInputSystem.Actions;

public sealed class InputAction
{
    public event Action
        OnPerformed = delegate { },
        OnCanceled = delegate { },
        OnStarted = delegate { };
    
    public string Name { get; set; }

    private ActionPhase _phase = ActionPhase.Waiting;

    [IgnoreDataMember]
    public ActionPhase ActionPhase
    {
        get => _phase;
        set
        {
            _phase = value;
            switch (_phase)
            {
                case ActionPhase.Waiting:
                    break;
                case ActionPhase.Started:
                    OnStarted.Invoke();
                    break;
                case ActionPhase.Performed:
                    OnPerformed.Invoke();
                    break;
                case ActionPhase.Canceled:
                    OnCanceled.Invoke();
                    break;
            }
        }
    }
    
    public InputValueType ValueType { get; set; }
    public List<InputTrigger> Triggers { get; set; } = new();
    
    public void Update()
    {
        if (ActionPhase == ActionPhase.Disabled) return;
    }

    // private bool PressedKey(InputBinding binding)
    // {
    //     if (Input.IsPhysicalKeyPressed(binding.Key)) return true;
    //     if (Input.IsMouseButtonPressed(binding.MouseButton)) return true;
    //     if (Input.IsJoyButtonPressed(0, binding.JoyButton)) return true;
    //     return false;
    // }
}