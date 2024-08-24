using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameEventsManager : MonoBehaviour
{
    public static GameEventsManager instance { get; private set; }

    private void Awake(){
        if (instance != null){
            Debug.LogError("Found more than one GameEvents Manager in the scene.");
        }
        instance = this;
    }

    //  Game events for player status.
    public event Action onUpdatePlayerStatusDisplay;
    public void UpdatePlayerStatusDisplay(){
        if (onUpdatePlayerStatusDisplay != null){
            onUpdatePlayerStatusDisplay();
        }
    }

    // Player death.
    public event Action onPlayerDeath;
    public void PlayerDeath(){
        if (onPlayerDeath != null){
            onPlayerDeath();
        }
    }

    // Game event for quest state.
    public event Action onUpdateQuestSlot;
    public void UpdateQuestSlot(){
        if (onUpdateQuestSlot != null){
            onUpdateQuestSlot();
        }
    }

    //  Game events for player inventory.
    public event Action onUpdateInventorySlot;
    public void UpdateInventorySlot(){
        if (onUpdateInventorySlot != null){
            onUpdateInventorySlot();
        }
    }

    //  Item used text for player inventory.
    public event Action<string> onUpdateItemUsedText;
    public void UpdateItemUsedText(string key){
        if (onUpdateItemUsedText != null){
            onUpdateItemUsedText(key);
        }
    }

    public event Action onItemCollected;
    public void ItemCollected() {
        if (onItemCollected != null) {
            onItemCollected();
        }
    }

    //  Game events for vendor inventory.
    public event Action onUpdateVendorInventorySlot;
    public void UpdateVendorInventorySlot(){
        if (onUpdateVendorInventorySlot != null){
            onUpdateVendorInventorySlot();
        }
    }

    //  Item purchase text for vendor.
    public event Action<string, int> onUpdateItemPurchaseText;
    public void UpdateItemPurchaseText(string key, int cost){
        if (onUpdateItemPurchaseText != null){
            onUpdateItemPurchaseText(key, cost);
        }
    }
}
