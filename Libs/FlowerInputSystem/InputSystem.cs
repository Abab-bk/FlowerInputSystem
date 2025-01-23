using FlowerInputSystem.Contexts;

namespace FlowerInputSystem;

public static class InputSystem
{
    #nullable disable
    internal static InputReader Reader { get; private set; }
    internal static IEnumerable<InputMap> InputMaps { get; private set; }
    #nullable enable
    
    public static void Initialize(IEnumerable<InputMap> maps)
    {
        Reader = new InputReader();
        InputMaps = maps;
    }

    public static void Update(float delta)
    {
        Reader.UpdateState();

        foreach (var inputMap in InputMaps)
        {
            inputMap.Update(delta);
        }
    }
}