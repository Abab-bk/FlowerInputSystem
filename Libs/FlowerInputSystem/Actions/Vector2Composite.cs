using Godot;

namespace FlowerInputSystem.Actions;

public class Vector2Composite(
    ref InputBinding up,
    ref InputBinding right,
    ref InputBinding down,
    ref InputBinding left
    ) : InputBindingComposite<Vector2>
{
    private readonly InputBinding _up = up;
    private readonly InputBinding _right = right;
    private readonly InputBinding _down = down;
    private readonly InputBinding _left = left;
    
    public override Vector2 ReadValue()
    {
        Vector2 result;

        var x = GetAxis(in _right, in _left);
        var y = GetAxis(in _up, in _down);
        
        result = new Vector2(x, y).Normalized();
        
        return result;
    }

    private float GetAxis(in InputBinding positive, in InputBinding negative)
    {
        if (Input.IsPhysicalKeyPressed(positive.Key) && !Input.IsPhysicalKeyPressed(negative.Key)) return 1f;
        if (!Input.IsPhysicalKeyPressed(positive.Key) && Input.IsPhysicalKeyPressed(negative.Key)) return -1f;
        return 0f;
    }
    
    public bool IsPressed() =>
        GetAxis(in _right, in _left) != 0f ||
        GetAxis(in _up, in _down) != 0f;
}