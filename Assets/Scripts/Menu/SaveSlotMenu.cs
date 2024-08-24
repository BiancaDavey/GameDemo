using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveSlotMenu : MonoBehaviour
{
    [Header("Menu Navigation")]
    [SerializeField] private MainMenu mainMenu;
    [Header("Menu Buttons")]
    [SerializeField] private Button backButton;
    private SaveSlot[] saveSlots;
    private bool loadMenuClicked = false;

    private void Awake(){
        saveSlots = this.GetComponentsInChildren<SaveSlot>();
    }

    public void GetSaveSlot(SaveSlot saveSlot){
        DisableMenuButtons();
        DataPersistence.instance.ChangeSelectedProfileId(saveSlot.GetProfileId());
        if (!loadMenuClicked){
            Debug.Log("GetSaveSlot() with loadMenuClicked: " + loadMenuClicked);
            DataPersistence.instance.NewGame();
        }
        //  Save game before loading a new scene.
        SaveGameLoadScene();
    }

    //  Save the game before loading a scene.
    private void SaveGameLoadScene(){
        DataPersistence.instance.SaveGame();
        SceneManager.LoadSceneAsync(DataPersistence.instance.GetSavedSceneName());
    }

    //  Delete button for saved games.
    public void Delete(SaveSlot saveSlot){
        DataPersistence.instance.Delete(saveSlot.GetProfileId());
        ActivateMenu(loadMenuClicked);
    }    

    public void ActivateMenu(bool loadMenuClicked){
        this.gameObject.SetActive(true);
        this.loadMenuClicked = loadMenuClicked;
        Dictionary<string, GameData> profilesGameData = DataPersistence.instance.GetAllSaveProfiles();
        foreach (SaveSlot saveSlot in saveSlots){
            GameData profileData = null;
            profilesGameData.TryGetValue(saveSlot.GetProfileId(), out profileData);
            saveSlot.SetData(profileData);
            //  If save slot empty, disable button.
            if (profileData == null && loadMenuClicked){
                saveSlot.SetInteractable(false);
            }
            else {
                saveSlot.SetInteractable(true);
            }
        }
    }

    public void DeactivateMenu(){
        this.gameObject.SetActive(false);
    }

    public void DisableMenuButtons(){
        foreach (SaveSlot saveSlot in saveSlots){
            saveSlot.SetInteractable(false);
        }
        backButton.interactable = false;
    }
}
