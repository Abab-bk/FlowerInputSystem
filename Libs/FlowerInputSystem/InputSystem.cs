using FlowerInputSystem.Actions;

namespace FlowerInputSystem;

public static class InputSystem
{
    private static List<ActionMap> _actionMaps = new List<ActionMap>();
    
    public static void Initialize(List<ActionMap> actionMaps)
    {
        _actionMaps = actionMaps;
    }

    public static void Update()
    {
        foreach (var actionMap in _actionMaps)
        {
            actionMap.Update();
        }
    }
}