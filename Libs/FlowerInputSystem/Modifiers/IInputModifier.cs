using VYaml.Annotations;

namespace FlowerInputSystem.Modifiers;

[YamlObject]
[YamlObjectUnion("!scale", typeof(ScaleModifier))]
public partial interface IInputModifier
{
    public void Apply();
}