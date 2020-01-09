using Microsoft.MixedReality.Toolkit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private GameObject superObject;
    [SerializeField] private GameObject roomEnvironment;

    private void Start()
    {
        //check if the app is running on HoloLens 2
        bool supportsArticulatedHands = false;

        IMixedRealityCapabilityCheck capabilityCheck = CoreServices.InputSystem as IMixedRealityCapabilityCheck;
        if (capabilityCheck != null)
        {
            supportsArticulatedHands = capabilityCheck.CheckCapability(MixedRealityCapability.ArticulatedHand);
        }

        //if we are on hololens, the environment should be disabled (except if we are in Unity editor)
        roomEnvironment.SetActive(!supportsArticulatedHands || Application.isEditor);
    }

    public void SpawnNewObject()
    {
        Instantiate(superObject, Camera.main.transform.forward * 2, Quaternion.identity);
    }
}
