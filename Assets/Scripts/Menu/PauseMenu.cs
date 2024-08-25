using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    public static bool pauseMenuActive = false;
    public GameObject pauseMenu;
    public int mainMenuSceneIndex = 0;
    [SerializeField] private DialogueRunner dialogueRunner;
    private bool isCurrentConversation = false;
    string buttonName;

    void Start(){
        dialogueRunner.onDialogueComplete.AddListener(EndConversation);
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.P)){
            if (pauseMenuActive){
                HidePauseMenu();
            }
            else {
                ActivatePauseMenu();
            }
        }
        if (Input.GetKeyDown(KeyCode.E) && pauseMenuActive){
            buttonName = EventSystem.current.currentSelectedGameObject.name;
            if (buttonName.Equals("ContinueButton")){
                HidePauseMenu();
            }
            else if (buttonName.Equals("SaveGameButton")){
                SaveGame();
            }
            else if (buttonName.Equals("ReturnToMainMenuButton")){
                LoadMainMenu();
            }
            else if (buttonName.Equals("ExitButton")){
                ExitGame();
            }
        }
    }

    public void ActivatePauseMenu() {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        pauseMenuActive = true;
    }

    public void HidePauseMenu(){
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        pauseMenuActive = false;
    }

    public void SaveGame(){
        DataPersistence.instance.SaveGame();
        if (!dialogueRunner.IsDialogueRunning){
            StartConversation("SaveGame");
        }
    }

    public void LoadMainMenu(){
        SceneManager.LoadScene(mainMenuSceneIndex);
        HidePauseMenu(); 
    }

    public bool PauseMenuActive(){
        return pauseMenuActive;
    }

    private void StartConversation(string node) {
        isCurrentConversation = true;
        dialogueRunner.StartDialogue(node);
    }

    private void EndConversation() {
        if (isCurrentConversation) {
            isCurrentConversation = false;
        }
    }

    public void ExitGame() {
        // UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
