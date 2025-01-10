using System.Runtime.Serialization;
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

    public List<InputBinding> Bindings { get; set; }
    
    private bool _wasKeyJustPressedLastFrame = false;
    
    public InputAction(List<InputBinding> bindings, string name = "")
    {
        Name = name;
        Bindings = bindings;
    }
    
    public InputAction()
    {
        Name = "";
        Bindings = [];
    }

    public void Update()
    {
        if (ActionPhase == ActionPhase.Disabled) return;
        
        foreach (var binding in Bindings)
        {
            if (Input.IsPhysicalKeyPressed(binding.Key) &&
                ActionPhase == ActionPhase.Waiting &&
                !_wasKeyJustPressedLastFrame
                )
            {
                ActionPhase = ActionPhase.Performed;
            }
            else if (ActionPhase == ActionPhase.Performed &&
                     Input.IsPhysicalKeyPressed(binding.Key)
                     )
            {
                ActionPhase = ActionPhase.Waiting;
            }
            
            _wasKeyJustPressedLastFrame = Input.IsPhysicalKeyPressed(binding.Key);
        }
    }
}