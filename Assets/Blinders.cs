using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Transition 
{
    BothSides,
    LeftSide,
    RightSide,
}
public class Blinders : MonoBehaviour
{
    [Header("Transition Settings")]
    public Transition transition;
    public Ease transitionEase;
    
    public Transform right, left;
    
    private float _duration = .3f;
    private bool _openAtStart;
    
    [Header("Loadingbar Settings")]
    public Ease loadingEase;
    public Transform loadingIcon;

    [Header("Camera")]
    public Camera camera;
    public float GetDuration => _duration;

    private void Start()
    {
        _openAtStart = SceneManager.sceneCount > 1;

        if (!_openAtStart) return;
        camera.gameObject.SetActive(false);
        Invoke(nameof(Open), .5f);
    }

    [ContextMenu("Open")]
    public void Open()
    {
        DoScaleX(0);
        loadingIcon.DOScale(new Vector3(0, 0, 0), 0.2f).SetEase(loadingEase);
    }


    [ContextMenu("Close")]
    public void Close()
    {
        var destination = transition == Transition.BothSides ? 1 : 2;
        DoScaleX(destination);
        loadingIcon.DOScale(new Vector3(1, 1, 1), 0.2f).SetEase(loadingEase);
    }


    private void DoScaleX(float endValue)
    {
        switch (transition)
        {
            case Transition.BothSides:
                right.DOScaleX(endValue, _duration).SetEase(transitionEase);
                left.DOScaleX(endValue, _duration).SetEase(transitionEase);
                break;
            case Transition.LeftSide:
                _duration = .3f;
                transitionEase = Ease.InCirc;
                left.DOScaleX(endValue, _duration).SetEase(transitionEase);
                break;
            case Transition.RightSide:
              _duration = .3f;
              transitionEase = Ease.InCirc;
              right.DOScaleX(endValue, _duration).SetEase(transitionEase);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
    }
}
