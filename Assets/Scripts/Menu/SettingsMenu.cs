using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [Header("Menu Navigation")]
    [SerializeField] private MainMenu mainMenu;
    [Header("Menu Buttons")]
    [SerializeField] private Button backButton;

    public void ActivateMenu(bool activateSettingsMenu){
        this.gameObject.SetActive(true);
    }
    public void DeactivateMenu(){
        this.gameObject.SetActive(false);
    }
}
