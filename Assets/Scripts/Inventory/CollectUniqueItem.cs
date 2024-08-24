using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectUniqueItem : CollectItem, IDataPersistence
{
    /*  
        Functions to: 
        *   Update player inventory quantity upon object interaction.
        *   Update player currency upon object interaction.
        *   Objects can only be collected once.
    */
    [SerializeField] private string id;
    [SerializeField] private int amountIfCurrency;

    [ContextMenu("Generate GUID for ID")]
    private void GenerateGUID(){
        id = System.Guid.NewGuid().ToString();
    }

    //  Update player inventory quantity and destroy object.
    public override void CollectThisItem() { 
        GameEventsManager.instance.ItemCollected();
        //  Update player currency.
        if (itemName.Equals("Currency")){
            playerInventory.ObtainCurrency(amountIfCurrency);
        }
        //  Update player inventory item.
        else {
            playerInventory.ObtainItem(itemName);
            Debug.Log("Collected inventory item: " + itemName);
        }
        collected = true;
        Destroy(this.gameObject);
        //  TODO: implement inventory full check. 
    } 

    public void SaveData(GameData data){
        if (data.collectableItems.ContainsKey(id)){
            data.collectableItems.Remove(id);
        }
        data.collectableItems.Add(id, collected);
    }

    public void LoadData(GameData data){
        data.collectableItems.TryGetValue(id, out collected);
        if (collected) {
            collected = true;
        }
    }
    
}
