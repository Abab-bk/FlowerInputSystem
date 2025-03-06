using FlowerInputSystem.Inputs;
using FlowerInputSystem.Values;
using Godot;

namespace FlowerInputSystem;

internal class InputReader
{
    private readonly HashSet<InputEventMouseButton> _mouseButtons = new();
    private readonly HashSet<InputEventKey> _keys = new();
    private readonly HashSet<InputEventJoypadMotion> _joyMotions = new();
    private readonly HashSet<InputEventJoypadButton> _joyButtons = new();

    public void InjectInput(InputEvent inputEvent)
    {
        if (inputEvent.IsReleased()) return;
        switch (inputEvent)
        {
            case InputEventJoypadButton joyButton:
            {
                _joyButtons.Add(joyButton);
                break;
            }
            case InputEventJoypadMotion joyMotion:
            {
                _joyMotions.Add(joyMotion);
                break;
            }
            case InputEventKey key:
            {
                _keys.Add(key);
                break;
            }
            case InputEventMouseButton mouseButton:
            {
                _mouseButtons.Add(mouseButton);
                break;
            }
        }
    }

    public IActionValue GetValue(IInput input)
    {
        switch (input)
        {
            case KeyboardInput keyboardInput:
            {
                var pressed = _keys.Any(
                    x =>
                        x.PhysicalKeycode == keyboardInput.Key &&
                        x.Pressed
                );
                return new BoolActionValue { Value = pressed };
            }
            case MouseButtonInput mouseButtonInput:
            {
                var pressed = _mouseButtons.Any(
                    x =>
                        x.ButtonIndex == mouseButtonInput.MouseButton &&
                        x.Pressed
                );
                return new BoolActionValue { Value = pressed };
            }
            case JoyButtonInput joyButtonInput:
            {
                var pressed = _joyButtons.Any(
                    x =>
                        x.ButtonIndex == joyButtonInput.Button &&
                        x.Pressed
                );
                return new BoolActionValue { Value = pressed };
            }
            case JoyAxisInput joyAxisInput:
            {
                var xEvent = _joyMotions
                    .FirstOrDefault(
                        x =>
                        x.Axis == joyAxisInput.X);
                var yEvent = _joyMotions
                    .FirstOrDefault(
                        x =>
                        x.Axis == joyAxisInput.Y);

                var x = xEvent?.AxisValue ?? 0f;
                var y = yEvent?.AxisValue ?? 0f;
                
                return new Axis2DActionValue { Value = new Vector2(x, y) };
            }
    
            default:
                throw new NotImplementedException();
        }
    }

    public void ConsumeAll()
    {
        _keys.Clear();
        _mouseButtons.Clear();
        _joyMotions.Clear();
        _joyButtons.Clear();
    }

    // public void Consume(IInput input)
    // {
    //     switch (input)
    //     {
    //         case KeyboardInput keyboardInput:
    //             _keys.RemoveAll(x => x.PhysicalKeycode ==
    //                                  keyboardInput.Key);
    //             return;
    //         case MouseButtonInput mouseButtonInput:
    //             _mouseButtons.RemoveAll(x =>
    //                 x.ButtonIndex == mouseButtonInput.MouseButton);
    //             return;
    //         case JoyButtonInput joyButtonInput:
    //             _joyButtons.RemoveAll(x =>
    //                 x.ButtonIndex == joyButtonInput.Button);
    //             return;
    //         case JoyAxisInput joyAxisInput:
    //             _joyMotions.RemoveAll(x =>
    //                 x.Axis == joyAxisInput.X ||
    //                 x.Axis == joyAxisInput.Y);
    //             return;
    //         default:
    //             throw new NotImplementedException();
    //     }
    // }
}