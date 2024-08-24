using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMenu : MonoBehaviour
{
    public static bool inventoryMenuActive = false;
    public GameObject inventoryMenu;

    void Update(){
        if (Input.GetKeyDown(KeyCode.I)){
            if (inventoryMenuActive){
                HideInventoryMenu();
            }
            else {
                ActivateInventoryMenu();  
            }
        }
    }

    public void ActivateInventoryMenu() {
        inventoryMenu.SetActive(true);
        Time.timeScale = 0f;
        inventoryMenuActive = true;
    }
    
    public void HideInventoryMenu(){
        inventoryMenu.SetActive(false);
        Time.timeScale = 1f;
        inventoryMenuActive = false;
    }

    public bool InventoryMenuActive(){
        return inventoryMenuActive;
    }
}
