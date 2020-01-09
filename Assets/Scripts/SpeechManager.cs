using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechManager : Singleton<SpeechManager>
{ 
    public void OnSpawnCommand()
    {
        GameManager.Instance.SpawnNewObject();
    }
}
