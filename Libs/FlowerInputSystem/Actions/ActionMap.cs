namespace FlowerInputSystem.Actions;

public class ActionMap
{
    public List<InputAction> Actions;

    public ActionMap(List<InputAction> actions)
    {
        Actions = actions;
    }

    public void Update()
    {
        foreach (var action in Actions)
        {
            action.Update();
        }
    }
}