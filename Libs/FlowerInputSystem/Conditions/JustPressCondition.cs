﻿using FlowerInputSystem.Actions;
using FlowerInputSystem.Values;
using VYaml.Annotations;

namespace FlowerInputSystem.Conditions;

[YamlObject]
public partial struct JustPressCondition() : IInputCondition
{
    private bool _previouslyActuated = false;
    public float Actuation { get; set; }
    
    public InputActionState Evaluate(IActionValue actionValue, float delta)
    {
        if (_previouslyActuated) return InputActionState.None;
        
        var pressed = actionValue.IsActuated(Actuation);
        var result = pressed && !_previouslyActuated ?
            InputActionState.Fired : InputActionState.None;
        _previouslyActuated = pressed;
        return result;
    }
}