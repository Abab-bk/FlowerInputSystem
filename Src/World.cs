using Godot;
using FlowerInputSystem;
using FlowerInputSystem.Actions;

namespace FlowerInputDemo;

public partial class World : Node2D
{
    private InputAction _inputAction = new InputAction([
    new InputBinding()
    {
        Key = Key.Space
    }
    ], "TestInputAction");
    private ActionMap _actionMap;
    
    public override void _Ready()
    {
        _inputAction.OnPerformed += () =>
        {
            GD.Print($"{_inputAction.Name} performed");
        };

        _actionMap = new ActionMap([_inputAction]);
        InputSystem.Initialize([_actionMap]);
    }

    public override void _Process(double delta)
    {
        InputSystem.Update();
    }
}
