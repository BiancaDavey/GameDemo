using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestMenu : MonoBehaviour
{
    public static bool questMenuActive = false;
    public GameObject questMenu;

    void Update(){
        if (Input.GetKeyDown(KeyCode.J)){
            if (questMenuActive){
                HideQuestMenu();
            }
            else {
                ActivateQuestMenu();  
            }
        }
    }

    public void ActivateQuestMenu() {
        questMenu.SetActive(true);
        Time.timeScale = 0f;
        questMenuActive = true;
    }
    
    public void HideQuestMenu(){
        questMenu.SetActive(false);
        Time.timeScale = 1f;
        questMenuActive = false;
    }

    public bool QuestMenuActive(){
        return questMenuActive;
    }
}
