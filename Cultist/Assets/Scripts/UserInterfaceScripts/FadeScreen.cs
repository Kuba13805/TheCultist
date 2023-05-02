using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeScreen : MonoBehaviour
{
    [SerializeField] private float fadeTime;
    private float _fadeSpeed;
    private float _currentAlpha;
    
    private Image _uiElement;

    private void Start()
    {
        TravelPoint.OnPlayerTravel += FadeScreenOnEvent;

        TravelPoint.OnPlayerTravelDone += ClearScreenOnEvent;
        
        _uiElement = GetComponent<Image>();
        _fadeSpeed = 1f / fadeTime;

        var tempColor = _uiElement.color;
        tempColor.a = 0f;
        _uiElement.color = tempColor;
    }

    private void OnDestroy()
    {
        TravelPoint.OnPlayerTravel -= FadeScreenOnEvent;
        
        TravelPoint.OnPlayerTravelDone -= ClearScreenOnEvent;
    }

    private void FadeScreenOnEvent()
    {
        InvokeRepeating(nameof(Fade), 0f, Time.deltaTime);
    }

    private void ClearScreenOnEvent()
    {
        InvokeRepeating(nameof(Clear), 0f, Time.deltaTime);
    }

    private void Fade()
    {
        _currentAlpha += _fadeSpeed * Time.deltaTime;

        _currentAlpha = Mathf.Clamp01(_currentAlpha);

        var newColor = _uiElement.color;
        newColor.a = _currentAlpha;
        _uiElement.color = newColor;

        if (_currentAlpha >= 1f)
        {
            CancelInvoke(nameof(Fade));
        }
    }

    private void Clear()
    {
        StartCoroutine(Wait(2f));
        _currentAlpha -= _fadeSpeed * Time.deltaTime;

        _currentAlpha = Mathf.Clamp01(_currentAlpha);
        
        var newColor = _uiElement.color;
        newColor.a = _currentAlpha;
        _uiElement.color = newColor;
        
        if (_currentAlpha == 0f)
        {
            CancelInvoke(nameof(Clear));
        }
    }

    private static IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
    }
}
