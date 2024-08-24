using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class SaveSlot : MonoBehaviour
{
    [Header("Profile")]
    [SerializeField] private string profileId = "";
    [Header("Content")]
    [SerializeField] private GameObject noData;
    [SerializeField] private GameObject hasData;
    [SerializeField] private TextMeshProUGUI timeSavedText;
    [SerializeField] private TextMeshProUGUI profileIdText;
    [SerializeField] private Button saveSlotButton;
    [Header("Delete Button")]
    [SerializeField] private Button deleteButton;
    [Header("Icon")]
    [SerializeField] private GameObject saveIcon;

    private void Awake(){
        saveSlotButton = this.GetComponent<Button>();
    }

    public void SetData(GameData data){
        if (data == null){
            noData.SetActive(true);
            hasData.SetActive(false);
            deleteButton.gameObject.SetActive(false);
        }
        else {
            noData.SetActive(false);
            hasData.SetActive(true);
            deleteButton.gameObject.SetActive(true);
            //  Display date and time saved for each SaveSlot.
            timeSavedText.text = DisplayTimeSaved(data.lastUpdated);
            //  Display profileId for each SaveSlot.
            profileIdText.text = "Save " + GetProfileId();
            //  Set icon to display for save slot.
            SetSaveIcon(data);
        }
    }

    //  Set icon to display for save slot dependent on current scene.
    public void SetSaveIcon(GameData data){
        Image saveIconImg = saveIcon.GetComponentInChildren<Image>();
        Sprite saveIconSprite = Resources.Load<Sprite>("Portraits/" + data.currentSceneName);
        saveIconImg.sprite = saveIconSprite;
    }

    public string GetProfileId(){
        return this.profileId;
    }

    public void SetInteractable(bool interactable){
        saveSlotButton.interactable = interactable;
        deleteButton.interactable = interactable;
    }

    //  Convert long to string to display date and time.
    public string DisplayTimeSaved(long dataDate){
        DateTime dateTimeSaved = DateTime.FromBinary(dataDate);
        string displayTimeSaved = dateTimeSaved.ToString("MM/dd/yyyy H:mm");
        return displayTimeSaved;
    }
}
