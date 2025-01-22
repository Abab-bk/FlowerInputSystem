using Godot;

namespace FlowerInputSystem.Inputs;

public struct MouseButtonInput : IInput
{
    public MouseButton MouseButton { get; set; }
}