using Godot;

namespace FlowerInputSystem.Actions;

public struct InputBinding
{
    public Key Key { get; set; }
    public MouseButton MouseButton { get; set; }
    public JoyButton JoyButton { get; set; }
    public JoyAxis JoyAxis { get; set; }
}