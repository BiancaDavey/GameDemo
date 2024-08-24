using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusDisplay : MonoBehaviour
{
    public static bool activateStatusDisplay = false;
    public GameObject statusDisplay;

    void Update(){
        if (Input.GetKeyDown(KeyCode.Tab)){
            Debug.Log("tab presed");
            if (activateStatusDisplay){
                HideStatusDisplay();
            }
            else {
                ActivateStatusDisplay();  
            }
        }
    }

    public void ActivateStatusDisplay() {
        statusDisplay.SetActive(true);
        activateStatusDisplay = true;
    }
    
    public void HideStatusDisplay(){
        statusDisplay.SetActive(false);
        activateStatusDisplay = false;
    }

    public bool StatusDisplayActive(){
        return activateStatusDisplay;
    }
}
