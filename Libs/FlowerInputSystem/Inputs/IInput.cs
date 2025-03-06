using FlowerInputSystem.Values;
using Godot;
using VYaml.Annotations;

namespace FlowerInputSystem.Inputs;

[YamlObject]
[YamlObjectUnion("!keyboard", typeof(KeyboardInput))]
[YamlObjectUnion("!mouse-button", typeof(MouseButtonInput))]
[YamlObjectUnion("!joy-axis", typeof(JoyAxisInput))]
[YamlObjectUnion("!joy-button", typeof(JoyButtonInput))]
public partial interface IInput
{
}