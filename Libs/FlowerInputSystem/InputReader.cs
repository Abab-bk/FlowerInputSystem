using FlowerInputSystem.Inputs;
using FlowerInputSystem.Values;
using Godot;

namespace FlowerInputSystem;

internal class InputReader
{
    private ConsumedInput _consumedInput = new ();
    
    public void UpdateState()
    {
        _consumedInput.Reset();
    }

    public IActionValue GetValue(IInput input)
    {
        if (input is KeyboardInput keyboardInput)
        {
            var pressed = Input.IsPhysicalKeyPressed(keyboardInput.Key) &&
                          !_consumedInput.Keys.Contains(keyboardInput.Key);
            return new BoolActionValue { Value = pressed };
        }
        
        if (input is MouseButtonInput mouseButtonInput)
        {
            var pressed = Input.IsMouseButtonPressed(mouseButtonInput.MouseButton) &&
                          !_consumedInput.MouseButtons.Contains(mouseButtonInput.MouseButton);
            return new BoolActionValue { Value = pressed };
        }
        
        throw new NotImplementedException();
    }

    public void Consume(IInput input)
    {
        if (input is KeyboardInput keyboardInput)
        {
            _consumedInput.Keys.Add(keyboardInput.Key);
            // TODO: Add mod keys.
            return;
        }
        
        if (input is MouseButtonInput mouseButtonInput)
        {
            _consumedInput.MouseButtons.Add(mouseButtonInput.MouseButton);
            return;
        }
    }
}