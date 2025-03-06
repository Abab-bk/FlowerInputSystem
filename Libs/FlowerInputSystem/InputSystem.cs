using FlowerInputSystem.Contexts;
using Godot;

namespace FlowerInputSystem;

public static class InputSystem
{
    #nullable disable
    internal static InputReader Reader { get; private set; }
    internal static IEnumerable<ActionMap> InputMaps { get; private set; }
    #nullable enable
    
    public static void Initialize(
        IEnumerable<ActionMap> maps
        )
    {
        Reader = new InputReader();
        InputMaps = maps;
    }

    public static void Update(float delta)
    {
        foreach (var inputMap in InputMaps)
        {
            inputMap.Update(delta);
        }
        
        Reader.ConsumeAll();
    }

    public static void InjectInput(InputEvent inputEvent)
    {
        Reader.InjectInput(inputEvent);
    }
}