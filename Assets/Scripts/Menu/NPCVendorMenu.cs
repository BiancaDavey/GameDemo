using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCVendorMenu : MonoBehaviour
{
    public GameObject vendorMenu;
    public PauseMenu pauseMenu;
    private bool triggerOn = false;
    public static bool vendorMenuActive = false;

    public void Update(){
        if (vendorMenuActive && Input.GetKeyDown(KeyCode.Q)){
            HideVendorMenu();
        }
    }

    public void ActivateVendorMenu(){
        vendorMenu.SetActive(true);
        Time.timeScale = 0f;
        vendorMenuActive = true;
    }

    public void HideVendorMenu(){
        vendorMenu.SetActive(false);
        Time.timeScale = 1f;
        vendorMenuActive = false;
    }

    public bool VendorMenuActive(){
        return vendorMenuActive;
    }
}
