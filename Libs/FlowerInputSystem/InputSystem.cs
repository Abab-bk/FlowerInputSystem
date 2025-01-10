using FlowerInputSystem.Actions;

namespace FlowerInputSystem;

public static class InputSystem
{
    private class ActionMapContainer(ActionMap actionMap)
    {
        public ActionMap ActionMap { get; } = actionMap;
        public bool Enabled { get; set; } = true;
        public bool TempDisabled { get; set; }
    }

    private static readonly List<ActionMapContainer> ActionMapContainers = new();

    public static void SetAllActionMapsTempDisabled(bool isDisabled)
    {
        foreach (var actionMapContainer in ActionMapContainers)
        {
            actionMapContainer.TempDisabled = isDisabled;
        }
    }

    public static bool SetActionMapEnabled(ActionMap actionMap, bool isEnabled)
    {
        var actionMapContainer = ActionMapContainers
            .Find(x => x.ActionMap == actionMap);
        if (actionMapContainer == null) return false;
        actionMapContainer.Enabled = isEnabled;
        return true;
    }
    
    public static void TempDisabledAllActionMaps() => SetAllActionMapsTempDisabled(true);
    public static void TempEnabledAllActionMaps() => SetAllActionMapsTempDisabled(false); 
    public static bool DisabledActionMap(ActionMap actionMap) =>
        SetActionMapEnabled(actionMap, false);
    public static bool EnabledActionMap(ActionMap actionMap) =>
        SetActionMapEnabled(actionMap, true);
    
    public static void Initialize(List<ActionMap> actionMaps)
    {
        foreach (var actionMap in actionMaps)
        {
            ActionMapContainers.Add(new ActionMapContainer(actionMap));
        }
    }
    
    public static void AddActionMap(ActionMap actionMap) =>
        ActionMapContainers.Add(new ActionMapContainer(actionMap));

    public static ActionMap? FindActionMap(string name) =>
        ActionMapContainers.Find(x => x.ActionMap.Name == name)?.ActionMap;
    
    public static void Update()
    {
        foreach (var actionMapContainer in ActionMapContainers)
        {
            if (!actionMapContainer.Enabled || actionMapContainer.TempDisabled) continue;
            actionMapContainer.ActionMap.Update();
        }
    }
}