namespace FlowerInputSystem.Actions;

public abstract class InputBindingComposite<T> where T : struct
{
    public abstract T ReadValue();
}