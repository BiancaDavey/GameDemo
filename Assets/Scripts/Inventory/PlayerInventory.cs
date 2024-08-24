using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;
using Yarn.Unity;

public class PlayerInventory : MonoBehaviour, IDataPersistence
{
    /*
        Functions to:
            * Update player inventory quantity upon obtaining or using an item.
            * Update player currency quantity upon obtaining or using currency.
            * Get quantity of an item.
            * Update player inventory menu slots when item quantity changes.
            * Display item used text on inventory menu for specified seconds.
            * Set and get item action status for buttons.
            * UseItem if item can be used.
    */
    //  Player inventory items dictionary.
    public Dictionary<string, int> playerItemsDictionary = new Dictionary<string, int>(){
        ["Currency"] = 0
    };
    //  Dictionary of items that update health status.
    public Dictionary<string, int> itemHealthUpdate = new Dictionary<string, int>(){
        ["Potion Health"] = 30,
        ["Berries"] = 5
    };
    //  Dictionary of items that update magicka status.
    public Dictionary<string, int> itemMagickaUpdate = new Dictionary<string, int>(){
        ["Potion Magicka"] = 30,
        ["Feather"] = 5
    };
    //  Items that cannot be used from the inventory menu.
    private List<string> questItem = new List<string>(){
        "Bellblossom"
    };
    int maxSlots = 12;
    int currentPlayerInventorySize;  //  TODO InventoryFull.
    [SerializeField] private Transform scrollViewContent;
    [SerializeField] public GameObject inventorySlotPrefab;
    public static PlayerInventory instance { get; private set; }
    [SerializeField] PlayerStatus playerStatus;
    [SerializeField] TextMeshProUGUI itemUsedText;
    [SerializeField] GameObject itemUsedTextObject;
    private bool itemAction = false;
    [SerializeField] public AudioSource getItemSFX;
    [SerializeField] public AudioSource useItemSFX;
    [SerializeField] public AudioSource currencySFX;

    void Start(){ 
        GameEventsManager.instance.onUpdateInventorySlot += OnUpdateInventorySlot;
        GameEventsManager.instance.onUpdateItemUsedText += OnUpdateItemUsedText;
    }

    private void Awake(){
        OnUpdateInventorySlot();
        itemUsedTextObject.SetActive(false);
    }
    
    private void OnDestroy(){
        GameEventsManager.instance.onUpdateInventorySlot -= OnUpdateInventorySlot;
        GameEventsManager.instance.onUpdateItemUsedText -= OnUpdateItemUsedText;
    }

    //  Function to update text in inventory menu when an item is used.
    public void OnUpdateItemUsedText(string key){
        itemUsedTextObject.SetActive(true);
        itemUsedText.text = "You used an item";
        StartCoroutine("HideUsedItemText");
    }

    //  Coroutine to hide text in inventory menu after an item is used.
    IEnumerator HideUsedItemText(){ 
        yield return new WaitForSecondsRealtime(1);
        itemUsedTextObject.SetActive(false);
    }

    //  Function to update inventory slots when an item is obtained or used.
    private void OnUpdateInventorySlot(){
        //  Destroy existing inventory slots.
        foreach (Transform child in scrollViewContent.transform){
            GameObject.Destroy(child.gameObject);
        }
        GameObject inventorySlot;
        int filledSlots = 0;
        //  Generate filled inventory slots.
        for (int i = 0; i < playerItemsDictionary.Count; i++){
            //  Render index 0 currency item with quantity text even if quantity 0 to retain currency at index 0.
            if (i == 0 || playerItemsDictionary.ElementAt(i).Value >= 1){ 
                filledSlots++;
                string itemName = playerItemsDictionary.ElementAt(i).Key;
                inventorySlot = (GameObject)Instantiate(inventorySlotPrefab, transform);
                //  Update text for item quantity and item name.
                TextMeshProUGUI[] itemTexts = inventorySlot.GetComponentsInChildren<TextMeshProUGUI>();
                itemTexts[0].text = playerItemsDictionary.ElementAt(i).Value.ToString();
                itemTexts[1].text = itemName;
                //  Update image for item.
                Image itemImage = inventorySlot.GetComponentInChildren<Image>();
                Sprite itemIcon = Resources.Load<Sprite>("Icons/" + itemName);
                itemImage.sprite = itemIcon; 
                Button itemButton = inventorySlot.GetComponentInChildren<Button>();
                //  Add MenuButton tag to inventory slot.
                itemButton.tag = "MenuButton";
                //  Link UseItem function to all items except index 0 currency item.
                if (i != 0){
                    itemButton.onClick.AddListener(()=> UseItem(itemName));
                }
            }
        }
        //  Generate empty inventory slots if filled slots are less than max slots.
        if (filledSlots < maxSlots){
            for (int j = 0; j < (maxSlots-filledSlots); j++){
                inventorySlot = (GameObject)Instantiate(inventorySlotPrefab, transform);
                //  Set image for empty inventory slot.
                Image itemImage = inventorySlot.GetComponentInChildren<Image>();
                Sprite itemIcon = Resources.Load<Sprite>("Icons/Empty");
                itemImage.sprite = itemIcon;
                //  Add buttons to all slots to enable selectable for last item in index.
                Button itemButton = inventorySlot.GetComponentInChildren<Button>();
                //  Add MenuButton tag to inventory slot.
                itemButton.tag = "MenuButton";
            }
        }
    }

    //  Function to set item action status for use in menu button script.
    public void SetItemActionStatus(bool status){
        itemAction = status;
    }

    //  Function to set item action status for use in menu button script.
    public bool GetItemActionStatus(){
        return itemAction;
    }

    //  Function to use an item.
    public void UseItem(string key){
        //  Check if item is in dictionary of items that cannot be used.
        if (questItem.Contains(key)){
            Debug.Log("Cannot use item.");
        }
        else {
            //  If item is in inventory dictionary and quantity is greater than or equal to 1, reduce the item quantity by 1.
            if (playerItemsDictionary.ContainsKey(key)){
                //  Check if quantity is less than 1.
                if (playerItemsDictionary[key] < 1){
                    Debug.Log("No more " + key + " left to use.");
                }
                else if (playerItemsDictionary[key] >= 1){
                    playerItemsDictionary[key] = playerItemsDictionary[key]-1;
                    useItemSFX.Play();
                    GameEventsManager.instance.UpdateInventorySlot();
                    GameEventsManager.instance.UpdateItemUsedText(key);
                    //  Update player status if item used is in the health or magicka status dictionary.
                    if (itemHealthUpdate.ContainsKey(key)){
                        playerStatus.UpdateStatus("Health", itemHealthUpdate[key]);
                    }
                    else if (itemMagickaUpdate.ContainsKey(key)){
                        playerStatus.UpdateStatus("Magicka", itemMagickaUpdate[key]);
                    }
                    SetItemActionStatus(true);
                }
            }
            else {
                Debug.Log("Error: item not found in player inventory dictionary.");
            }
        }
    }

    //  Function to obtain an item.
    public void ObtainItem(string key){
        //  If item is in inventory dictionary, quantity is increased by 1.
        if (playerItemsDictionary.ContainsKey(key)){
            playerItemsDictionary[key] = playerItemsDictionary[key]+1;
        }
        //  If item is not in inventory dictionary, item is added to dictionary with quantity 1.
        else {
            playerItemsDictionary.Add(key, 1);
        }
        getItemSFX.Play();
        GameEventsManager.instance.UpdateInventorySlot();
    }

    //  Function to obtain currency increase of specified amount.
    public void ObtainCurrency(int amount){
        playerItemsDictionary["Currency"] = playerItemsDictionary["Currency"]+amount;
        currencySFX.Play();
        GameEventsManager.instance.UpdateInventorySlot();
    }

    //  Function to use currency item in dictionary, for use with a vendor purchase.
    public void UseCurrency(int amount){
        if (playerItemsDictionary["Currency"] >= amount){
            playerItemsDictionary["Currency"] = playerItemsDictionary["Currency"]-amount;
            currencySFX.Play();
            GameEventsManager.instance.UpdateInventorySlot();
        }
        else {
            Debug.Log("Error- not enough currency.");
        }
    }

    //  Return item quantity if key in inventory dictionary, else return 0.
    public int GetItemQuantity(string key){
        if (playerItemsDictionary.ContainsKey(key)){
            return playerItemsDictionary[key];     
        }
        else {
            return 0;
        }
    }

    //  Update item quantity.
    public void UpdateQuantity(string key, int qty, bool remove, bool minus){
        if (playerItemsDictionary.ContainsKey(key)){
            //  If remove is true, remove the item from player inventory.
            if (remove){
                playerItemsDictionary.Remove(key);
                //  Update quest slots in menu.
                GameEventsManager.instance.UpdateInventorySlot();
            }
            else {
                //  Calculate new quantity for item.
                if (minus){
                    int newQty = playerItemsDictionary[key] - qty;
                    //  If new quantity is less than 1, remove the item from player inventory.
                    if (newQty < 1){
                        playerItemsDictionary.Remove(key);
                    }
                    //  If new quantity is greater than or equal to 1, update the item quantity in the inventory dictionary.
                    else {
                        playerItemsDictionary[key] = newQty;
                    }
                }
                else {
                    int newQty = playerItemsDictionary[key] + qty;
                    playerItemsDictionary[key] = newQty;
                }
            }
            //  Update quest slots in menu.
            GameEventsManager.instance.UpdateInventorySlot();
        }
        else {
            //  Add item to dictionary if not there and if not remove or minus.
            if (!minus && !remove){
                playerItemsDictionary.Add(key, qty);
                //  Update quest slots in menu.
                GameEventsManager.instance.UpdateInventorySlot();
            }
        }
    }

    //  TODO InventoryFull 
    //  Check whether player inventory is full with item not in dictionary.
    public bool InventoryFull(string key){
        if (currentPlayerInventorySize >= maxSlots && !playerItemsDictionary.ContainsKey(key)){
            return true;
        }
        else {
            return false;
        }
    }

    public void LoadData(GameData data){
        for (int i = 0; i < data.playerInventory.Count; i++){
            playerItemsDictionary[data.playerInventory.ElementAt(i).Key] = data.playerInventory.ElementAt(i).Value;
        }
    }

    public void SaveData(GameData data){
        for (int i = 0; i < playerItemsDictionary.Count; i++){ 
            data.playerInventory[playerItemsDictionary.ElementAt(i).Key] = playerItemsDictionary.ElementAt(i).Value;
        }
    }
}
