namespace FlowerInputSystem.Actions;

public class ActionMap
{
    public string Name { get; set; } = "";
    public List<InputAction> Actions { get; set; }

    public InputAction? FindAction(string name) => Actions.Find(x => x.Name == name);

    public ActionMap(List<InputAction> actions)
    {
        Actions = actions;
    }

    public ActionMap()
    {
        Actions = [];
    }

    public void Update()
    {
        foreach (var action in Actions)
        {
            action.Update();
        }
    }
}