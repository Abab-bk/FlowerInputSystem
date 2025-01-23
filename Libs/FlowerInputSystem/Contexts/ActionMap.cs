using FlowerInputSystem.Actions;
using VYaml.Annotations;
using VYaml.Serialization;

namespace FlowerInputSystem.Contexts;

[YamlObject]
public partial class ActionMap(string name, List<InputAction> actions)
{
    public string Name { get; set; } = name;
    public List<InputAction> Actions { get; set; } = actions;

    public void Update(float delta)
    {
        foreach (var inputAction in Actions)
        {
            inputAction.Update(delta);
        }
    }
    
    public InputAction? FindAction(string name) =>
        Actions.FirstOrDefault(x => x.Name == name);

    public static ActionMap CreateFromYaml(string yaml)
    {
        return YamlSerializer.Deserialize<ActionMap>(
            System.Text.Encoding.UTF8.GetBytes(yaml)
            );
    }
    
    public static async Task<ActionMap> CreateFromYamlAsync(Stream stream)
    {
        return await YamlSerializer.DeserializeAsync<ActionMap>(stream);
    }
}