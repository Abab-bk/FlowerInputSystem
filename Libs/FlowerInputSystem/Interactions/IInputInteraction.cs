namespace FlowerInputSystem.Interactions;

public interface IInputInteraction
{
    public void Process(ref InteractionContext context);
    public void Reset();
}