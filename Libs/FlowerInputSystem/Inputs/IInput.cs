using VYaml.Annotations;

namespace FlowerInputSystem.Inputs;

[YamlObject]
[YamlObjectUnion("!keyboard", typeof(KeyboardInput))]
[YamlObjectUnion("!mouse-button", typeof(MouseButtonInput))]
public partial interface IInput
{
    
}