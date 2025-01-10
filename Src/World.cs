using System;
using System.IO;
using Godot;
using FlowerInputSystem;
using FlowerInputSystem.Actions;
using Newtonsoft.Json;

namespace FlowerInputDemo;

public partial class World : Node2D
{
    [Export] private Button DisableAllInputMapBtn { get; set; }
    [Export] private Button EnableAllInputMapBtn { get; set; }
    [Export] private Label LoggerLabel { get; set; }

    private InputAction _inputAction;
    private ActionMap _actionMap;
    
    public override void _Ready()
    {
        _actionMap = JsonConvert.DeserializeObject<ActionMap>(
            File.ReadAllText("DefaultMap.json")
            );

        _inputAction = _actionMap.FindAction("Space or Right");

        if (_inputAction == null) throw new Exception("Input action not found");
        
        _inputAction.OnStarted += () =>
        {
            Log($"{_inputAction.Name} started");
        };
        _inputAction.OnCanceled += () =>
        {
            Log($"{_inputAction.Name} canceled");
        };
        _inputAction.OnPerformed += () =>
        {
            Log($"{_inputAction.Name} performed");
        };
        DisableAllInputMapBtn.Pressed += () =>
        {
            InputSystem.DisabledActionMap(_actionMap);
            Log("All input maps disabled");
        };
        EnableAllInputMapBtn.Pressed += () =>
        {
            InputSystem.EnabledActionMap(_actionMap);
            Log("All input maps enabled");
        };

        InputSystem.Initialize([]);
        InputSystem.AddActionMap(_actionMap);
    }

    private void Log(string message)
    {
        LoggerLabel.Text = message;
    }

    public override void _Process(double delta)
    {
        InputSystem.Update();
    }
}