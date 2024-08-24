using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightTrigger : MonoBehaviour
{
    /*
        Adjust light intensity depending on quest check.
    */
    [SerializeField] public Light2D light;
    [SerializeField] public QuestLog questLog;
    [SerializeField] public float[] intensityArray;
    [SerializeField] public string[] questValueArray;

    public void Start(){
        // OldAdjustIntensity(newIntensity);
        AdjustIntensity();
    }

    public void AdjustIntensity(){
        for (int i = 0; i < questValueArray.Length; i++){
            if (questLog.CheckQuestState(questValueArray[i])){
                light.intensity = intensityArray[i];
                break;
            }
        }
    }
}

