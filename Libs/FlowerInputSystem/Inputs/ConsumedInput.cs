using Godot;

namespace FlowerInputSystem.Inputs;

/// <summary>
/// Tracks all consumed inputs.
/// </summary>
internal struct ConsumedInput()
{
    public HashSet<Key> Keys = [];
    public HashSet<MouseButton> MouseButtons = [];
    
    // public bool MouseMotion;
    // public bool MouseWheel;
    
    public void Reset()
    {
        Keys.Clear();
        MouseButtons.Clear();
        // MouseMotion = false;
        // MouseWheel = false;
    }
}