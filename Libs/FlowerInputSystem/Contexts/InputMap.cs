using FlowerInputSystem.Actions;

namespace FlowerInputSystem.Contexts;

public class InputMap(string name, IEnumerable<InputAction> inputActions)
{
    public string Name => name;
    public IEnumerable<InputAction> Actions => inputActions;

    public void Update(float delta)
    {
        foreach (var inputAction in Actions)
        {
            inputAction.Update(delta);
        }
    }
}