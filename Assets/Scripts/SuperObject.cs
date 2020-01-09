using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperObject : MonoBehaviour, IMixedRealityPointerHandler, IMixedRealitySpeechHandler
{
    private Rigidbody _rigidbody;
    private MeshRenderer _renderer;
    private bool isColorChanging = false;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _renderer = GetComponent<MeshRenderer>();
    }

    #region "Pointer Handlers"
    public void OnPointerDown(MixedRealityPointerEventData eventData)
    {
        //Start move with far interaction: disable gravity
        if (eventData.Pointer is ShellHandRayPointer)
        {
            _rigidbody.useGravity = false;
        }
    }
    public void OnPointerUp(MixedRealityPointerEventData eventData)
    {
        //Start grab with near interaction: enable gravity to make the object throwable :)
        if (eventData.Pointer is SpherePointer)
        {
            _rigidbody.useGravity = true;
        }
    }
    public void OnPointerClicked(MixedRealityPointerEventData eventData) { }
    public void OnPointerDragged(MixedRealityPointerEventData eventData) { }
    #endregion region

    #region "Speech Handlers"
    public void OnSpeechKeywordRecognized(SpeechEventData eventData)
    {
        switch (eventData.Command.Keyword)
        {
            case "change color":
                isColorChanging = true;
                Debug.Log("Color Changing....");
                break;
            case "red":
                ChangeColor(Color.red);
                break;
            case "green":
                ChangeColor(Color.green);
                break;
            case "yellow":
                ChangeColor(Color.yellow);
                break;
            case "white":
                ChangeColor(Color.white);
                break;
        }
    }
    #endregion

    #region "Private Methods"
    private void ChangeColor(Color color)
    {
        if (!isColorChanging) return;

        _renderer.material.color = color;
        Debug.Log($"Color Changed to {color.ToString()}");
        isColorChanging = false;
    }
    #endregion
}
