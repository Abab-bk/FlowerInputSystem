namespace FlowerInputSystem.Interactions;

public class HoldInteraction : IInputInteraction
{
    public float Duration { get; set; }
    
    private float DurationOrDefault => Duration > 0 ? Duration : 1f;
    
    private float _currentDuration;
    
    public void Process(ref InteractionContext context)
    {
        switch (context.ActionPhase)
        {
            case ActionPhase.Waiting:
                _currentDuration = context.CurrentTime;
                context.Started();
                context.SetTimeout(DurationOrDefault);
                break;
            case ActionPhase.Performed:
                break;
            case ActionPhase.Canceled:
                break;
            case ActionPhase.Disabled:
                break;
        }
    }

    public void Reset()
    {
        _currentDuration = 0f;
    }
}