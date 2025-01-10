using FlowerInputSystem.Actions;

namespace FlowerInputSystem.Interactions;

public struct InteractionContext
{
    public InputAction Action { get; set; }
    public bool IsStarted { get; set; }
    public bool IsWaiting { get; set; }
    
    /// <summary>
    /// InteractionPhase
    /// </summary>
    public ActionPhase ActionPhase { get; set; }
    
    public float StartTime { get; set; }
    public float CurrentTime { get; set; }
    public float TimeHasExpired { get; set; }

    public void Started()
    {
    }
    
    public void Waiting()
    {
    }
    
    public void Performed()
    {
    }
    
    public void Canceled()
    {
    }

    public void SetTimeout(float seconds)
    {
    }
    
    public void SetTotalTimeout(float seconds)
    {
    }
}