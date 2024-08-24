using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXTrigger : MonoBehaviour
{
    [SerializeField] public AudioSource SFX;
    [HideInInspector] public bool triggerOn = false;

    public void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")){
            triggerOn = true;
        }
    }

    public void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")){
            triggerOn = false;
        }
    }

    public virtual void Update(){
        if (triggerOn && Input.GetKeyDown(KeyCode.E)){
            TriggerSFX();
        }
    }

    public void TriggerSFX(){
        if (SFX != null){
            SFX.Play();
        }
    }
}
