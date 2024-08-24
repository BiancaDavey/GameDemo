using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectItem : MonoBehaviour
{
    /*  
        Functions to: 
        *   Update player inventory quantity upon object interaction.
        *   Objects will regenerate upon scene reload.
    */
    [SerializeField] public string itemName;
    [SerializeField] public PlayerInventory playerInventory;
    [HideInInspector] public bool triggerOn = false;
    [HideInInspector] public bool collected = false;

    public void OnTriggerEnter2D(Collider2D other) {
        if (!collected && other.CompareTag("Player")){
            triggerOn = true;
        }
    }

    public void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")){
            triggerOn = false;
        }
    }

    public void Update(){
        if (collected){
            Destroy(this.gameObject);
        }
        if (triggerOn && Input.GetKeyDown(KeyCode.E) && !collected){
            CollectThisItem();
        }
    }

    //  Update player inventory quantity and destroy object.
    public virtual void CollectThisItem() { 
        GameEventsManager.instance.ItemCollected();
        playerInventory.ObtainItem(itemName);
        collected = true;
        Destroy(this.gameObject); 
        //  TODO: implement inventory full check.
    }
}
