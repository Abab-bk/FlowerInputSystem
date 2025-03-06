using Godot;
using VYaml.Annotations;

namespace FlowerInputSystem.Inputs;

[YamlObject]
public partial struct JoyAxisInput() : IInput
{
    public JoyAxis X { get; set; }
    public JoyAxis Y { get; set; }
}