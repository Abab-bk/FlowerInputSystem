using Godot;
using VYaml.Annotations;

namespace FlowerInputSystem.Inputs;

[YamlObject]
public partial struct KeyboardInput(Key key) : IInput
{
    public Key Key { get; set; } = key;
}