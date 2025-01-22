using FlowerInputSystem.Actions;

namespace FlowerInputSystem.Contexts;

public class InputMap(string name, IEnumerable<InputAction> inputActions)
{
    public string Name => name;
    public IEnumerable<InputAction> Actions => inputActions;

    public void Update()
    {
        foreach (var inputAction in Actions)
        {
            inputAction.Update();
        }
    }
}