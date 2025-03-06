using Godot;
using VYaml.Annotations;

namespace FlowerInputSystem.Inputs;

[YamlObject]
public partial struct JoyButtonInput() : IInput
{
    public JoyButton Button { get; set; }
}