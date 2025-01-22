using Godot;

namespace FlowerInputSystem.Inputs;

public struct KeyboardInput(Key key) : IInput
{
    public Key Key { get; set; } = key;
}