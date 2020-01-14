using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBehaviour : MonoBehaviour, IMixedRealityPointerHandler, IMixedRealitySpeechHandler
{
    private Rigidbody _rigidbody;
    private MeshRenderer _renderer;
    [SerializeField] private SpeechConfirmationTooltip speechConfirmationTooltipPrefab = null;

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
                Debug.Log("Color Changing....");
                ChangeColor();
                break;
            case "make bigger":
                transform.localScale *= 1.2f;
                break;
            case "make smaller":
                transform.localScale *= 0.8f;
                break;
        }
    }
    #endregion

    #region "Private Methods"
    private void ChangeColor()
    {
        var newColor = UnityEngine.Random.ColorHSV();
        var tooltipMesage = $"Color Changed to {newColor.ToString()}";

        //change color with a random color
        _renderer.material.color = newColor;
        Debug.Log(tooltipMesage);
        
        //show the tooltip
        var tooltipInstance = Instantiate(speechConfirmationTooltipPrefab);
        tooltipInstance.SetText(tooltipMesage);
        tooltipInstance.TriggerConfirmedAnimation();
    }
    #endregion
}
