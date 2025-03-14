using System;
using System.IO;
using Godot;
using FlowerInputSystem;
using FlowerInputSystem.Actions;
using FlowerInputSystem.Contexts;

namespace FlowerInputDemo;

public partial class World : Node2D
{
    [Export] private Button DisableAllInputMapBtn { get; set; }
    [Export] private Button EnableAllInputMapBtn { get; set; }
    [Export] private Label LoggerLabel { get; set; }

    private InputAction
        _moveAction,
        _pressAction,
        _justPressAction,
        _holdAction;
    private ActionMap _actionMap;
    
    public override void _Ready()
    {
        _actionMap = ActionMap.CreateFromYaml(File.ReadAllText("DefaultMap.yaml"));

        _moveAction = _actionMap.FindAction("Move");
        _pressAction = _actionMap.FindAction("PressK");
        _justPressAction = _actionMap.FindAction("JustPressL");
        _holdAction = _actionMap.FindAction("Hold-Space-1s");
        
        if (_pressAction == null ||
            _justPressAction == null ||
            _holdAction == null ||
            _moveAction == null
            )
        {
            throw new Exception("Actions not found");
        }
        
        _pressAction.OnFired += () =>
        {
            Log($"{_pressAction.Name} fired");
        };
        _justPressAction.OnFired += () =>
        {
            Log($"{_justPressAction.Name} fired");
        };
        _holdAction.OnFired += () =>
        {
            Log($"{_holdAction.Name} fired");
        };
        _moveAction.OnFired += () =>
        {
            Log($"{_moveAction.Name} fired, value: {_moveAction.Axis2DValue}");
        };
        _holdAction.OnGoing += () =>
        {
            Log($"{_holdAction.Name} going");
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
        
        InputSystem.Initialize([_actionMap]);
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
    
    public override void _UnhandledInput(InputEvent @event)
    {
        InputSystem.InjectInput(@event);
    }
}