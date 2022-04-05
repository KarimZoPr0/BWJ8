using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPlayer : MonoBehaviour
{
    public Animator anim;
    private string _currentState;
    public void ChangeAnimState(string newState)
    {
        if (_currentState == newState) return;
        
        var newHashState = Animator.StringToHash(newState);
        anim.Play(newHashState);
        
        _currentState = newState;
    }

}
