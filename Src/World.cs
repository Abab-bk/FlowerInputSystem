using Godot;
using FlowerInputSystem;
using FlowerInputSystem.Actions;
using FlowerInputSystem.Binds;
using FlowerInputSystem.Conditions;
using FlowerInputSystem.Inputs;
using InputMap = FlowerInputSystem.Contexts.InputMap;

namespace FlowerInputDemo;

public partial class World : Node2D
{
    [Export] private Button DisableAllInputMapBtn { get; set; }
    [Export] private Button EnableAllInputMapBtn { get; set; }
    [Export] private Label LoggerLabel { get; set; }

    private InputAction _inputAction;
    private InputMap _inputMap;
    
    public override void _Ready()
    {
        _inputAction = new InputAction(
            "Pressed Space",
            [new InputBind()
            {
                Input = new KeyboardInput(Key.Space),
                Conditions = [new PressCondition()]
            }],
            [],
            []
            );
        _inputMap = new InputMap(
            "Default",
            [_inputAction]
            );
        
        // _inputAction.OnStarted += () =>
        // {
        //     Log($"{_inputAction.Name} started");
        // };
        // _inputAction.OnCanceled += () =>
        // {
        //     Log($"{_inputAction.Name} canceled");
        // };
        _inputAction.OnFired += () =>
        {
            Log($"{_inputAction.Name} fired");
        };
        // DisableAllInputMapBtn.Pressed += () =>
        // {
        //     // InputSystem.DisabledActionMap(_actionMap);
        //     Log("All input maps disabled");
        // };
        // EnableAllInputMapBtn.Pressed += () =>
        // {
        //     // InputSystem.EnabledActionMap(_actionMap);
        //     Log("All input maps enabled");
        // };
        
        InputSystem.Initialize([_inputMap]);
    }

    private void Log(string message)
    {
        LoggerLabel.Text = message;
        GD.Print(message);
    }

    public override void _Process(double delta)
    {
        InputSystem.Update((float)delta);
    }
}