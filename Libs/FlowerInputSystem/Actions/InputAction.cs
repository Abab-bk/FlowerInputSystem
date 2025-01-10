using Godot;

namespace FlowerInputSystem.Actions;

public sealed class InputAction
{
    public event Action OnPerformed;
    
    public string Name { get; set; }

    private ActionPhase _phase = ActionPhase.Waiting;

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
                    break;
                case ActionPhase.Performed:
                    OnPerformed?.Invoke();
                    break;
                case ActionPhase.Canceled:
                    break;
            }
        }
    }

    private readonly List<InputBinding> _bindings;

    public InputAction(
        List<InputBinding> bindings,
        string name = ""
        )
    {
        Name = name;
        _bindings = bindings;
    }

    public void Update()
    {
        if (ActionPhase == ActionPhase.Disabled) return;
        
        if (ActionPhase == ActionPhase.Performed) ActionPhase = ActionPhase.Waiting;
        
        foreach (var binding in _bindings)
        {
            if (Input.IsPhysicalKeyPressed(binding.Key) &&
                ActionPhase == ActionPhase.Waiting
                )
            {
                ActionPhase = ActionPhase.Performed;
            }
        }
    }
}