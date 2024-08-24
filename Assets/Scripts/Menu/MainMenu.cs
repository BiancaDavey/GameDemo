using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Menu Navigation")]
    [SerializeField] private SaveSlotMenu saveSlotMenu;
    [SerializeField] private SettingsMenu settingsMenu;
    [Header("Menu Buttons")]
    [SerializeField] private Button ResumeGameButton;
    [SerializeField] private Button NewGameButton;
    [SerializeField] private Button LoadMenuButton;
    [SerializeField] private Button SettingsMenuButton;

    private void Start(){
        DisableButtonsBasedOnData();
    }

    private void DisableButtonsBasedOnData(){
        if (!DataPersistence.instance.GameDataExists()){
            ResumeGameButton.interactable = false;
            LoadMenuButton.interactable = false;
        }
    }

    public void NewGame(){
        saveSlotMenu.ActivateMenu(false);
        this.DeactivateMenu();
    }

    public void ResumeGame(){
        //  Save game before loading a new scene.
        DataPersistence.instance.SaveGame();
        SceneManager.LoadSceneAsync(DataPersistence.instance.GetSavedSceneName());
    }

    public void ExitGame() {
        //  Save upon exit.
        DataPersistence.instance.SaveGame();
        Application.Quit();
    }

    //  Function to test exit game in editor. 
    public void ExitGameTest(){
        Debug.Log("Exit game test mode.");
        DataPersistence.instance.SaveGame();
        //  Reference to EditorApp is for editor only; not in build.
        // UnityEditor.EditorApplication.isPlaying = false;
    }

    //  Display the main menu.
    public void ActivateMenu(){
        this.gameObject.SetActive(true);
        DisableButtonsBasedOnData();
    }

    //  Hide the main menu.
    public void DeactivateMenu(){
        this.gameObject.SetActive(false);
    }

    //  Display the load menu.
    public void ActivateLoadMenu(){
        saveSlotMenu.ActivateMenu(true);
        this.DeactivateMenu();
    }

    //  Display the settings menu.
    public void ActivateSettingsMenu(){
        settingsMenu.ActivateMenu(true);
        this.DeactivateMenu();
    }
}

