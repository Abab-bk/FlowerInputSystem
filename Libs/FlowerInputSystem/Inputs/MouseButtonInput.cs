using Godot;
using VYaml.Annotations;

namespace FlowerInputSystem.Inputs;

[YamlObject]
public partial struct MouseButtonInput() : IInput
{
    public MouseButton MouseButton { get; set; }
}