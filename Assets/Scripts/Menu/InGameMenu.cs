using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMenu : MonoBehaviour
{
    [Header("Menu Navigation")]
    [SerializeField] private bool simpleMenu;
    [SerializeField] private PauseMenu pauseMenu;
    [SerializeField] private QuestMenu questMenu;
    [SerializeField] private StatusDisplay statusDisplay;
    [SerializeField] private InventoryMenu inventoryMenu;
    [SerializeField] private bool vendorMenuScene;
    private NPCVendorMenu npcVendorMenu;
    [HideInInspector] public bool pauseMenuActive = false;
    [HideInInspector] public bool questMenuActive = false;
    [HideInInspector] public bool statusDisplayActive = false;
    [HideInInspector] public bool inventoryMenuActive = false;

    void Start(){
        npcVendorMenu = FindObjectOfType<NPCVendorMenu>();
    }

    void Update(){
        //  Activate/Hide PauseMenu if P pressed.
        if (Input.GetKeyDown(KeyCode.P)){
            if (pauseMenuActive){
                pauseMenu.HidePauseMenu();
            }
            else {
                pauseMenu.ActivatePauseMenu();
            }
        }
        
        //  Activate/Hide Quest menu if J pressed.
        if (Input.GetKeyDown(KeyCode.J) && !simpleMenu){
            if (questMenuActive){
                questMenu.HideQuestMenu();
            }
            else {
                //  If PauseMenu active, don't activate quest menu.
                if (pauseMenu.PauseMenuActive()){
                    Debug.Log("Close pause menu first.");
                }
                else {
                    if (!vendorMenuScene){
                        if (inventoryMenu.InventoryMenuActive()){
                            inventoryMenu.HideInventoryMenu();
                        }
                        //  Activate quest menu.
                        questMenu.ActivateQuestMenu();
                    }
                    else {
                        if (!npcVendorMenu.VendorMenuActive()){
                            if (inventoryMenu.InventoryMenuActive()){
                                inventoryMenu.HideInventoryMenu();
                            }
                            //  Activate quest menu.
                            questMenu.ActivateQuestMenu();
                        }
                    }
                }
            }  
        }

        //  Activate/Hide Status display if TAB pressed.
        if (Input.GetKeyDown(KeyCode.Tab) && !simpleMenu){
            if (statusDisplayActive){
                statusDisplay.HideStatusDisplay();
            }
            else {
                //  If PauseMenu active, don't activate status.
                if (pauseMenu.PauseMenuActive()){
                    Debug.Log("Close pause menu first.");
                }
                else { 
                    statusDisplay.ActivateStatusDisplay();
                }
            }  
        }

        //  Activate/Hide Inventory Test menu if I pressed.
        if (Input.GetKeyDown(KeyCode.I) && !simpleMenu){
            if (inventoryMenuActive){
                inventoryMenu.HideInventoryMenu();
            }
            else {
                //  If PauseMenu active, don't activate inventory menu.
                if (pauseMenu.PauseMenuActive()){
                    Debug.Log("Close pause menu first.");
                }
                else {
                    if (!vendorMenuScene){
                        if (questMenu.QuestMenuActive()){
                            questMenu.HideQuestMenu();
                        }
                        //  Activate inventory menu.
                        inventoryMenu.ActivateInventoryMenu(); 
                    }
                    else {
                        if (!npcVendorMenu.VendorMenuActive()){
                            if (questMenu.QuestMenuActive()){
                                questMenu.HideQuestMenu();
                            }
                            //  Activate inventory menu.
                            inventoryMenu.ActivateInventoryMenu();
                        }
                    }
                }
            }  
        }
    }

    //  Return true if menu active.
    public bool GamePaused(){
        if (!vendorMenuScene){
            if (pauseMenu.PauseMenuActive() || questMenu.QuestMenuActive() || inventoryMenu.InventoryMenuActive()){
                return true;
            }
            else {
                return false;
            }
        }
        else {
            if (pauseMenu.PauseMenuActive() || questMenu.QuestMenuActive() || inventoryMenu.InventoryMenuActive() || npcVendorMenu.VendorMenuActive()){
                return true;
            }
            else {
                return false;
            }            
        }
    }
}
