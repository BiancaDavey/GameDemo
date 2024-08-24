using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.UI;

public class YarnInteractable : MonoBehaviour
{
    //  Functions to run dialogue upon interaction with object.
    [SerializeField] public string node;
    [SerializeField] public string portraitName;
    //  Disable player movement if applicable and always if options are given.
    [SerializeField] public bool disablePlayerMovement;
    [SerializeField] public PlayerController playerController;
    [SerializeField] public GameObject portraitArea;
    [SerializeField] public LineView lineView;
    [HideInInspector] public DialogueRunner dialogueRunner;
    [HideInInspector] public bool interactable = true;
    [HideInInspector] public bool isCurrentConversation = false;
    [HideInInspector] public bool triggerOn = false;
    [SerializeField] public AudioSource SFX;
    
    public void Start() {
        dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
        dialogueRunner.onDialogueComplete.AddListener(EndConversation);
    }

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

    //  Activate portrait if not empty.
    public void ActivatePortrait(){
        if (portraitName.Equals("")){
            portraitArea.SetActive(false);
        }
        else {
            portraitArea.SetActive(true);
            SetPortrait(portraitName);
        }
    }

    //  Set portrait.
    public void SetPortrait(string portraitName){
        Image portraitImage = portraitArea.GetComponentInChildren<Image>();
        Sprite portraitIcon = Resources.Load<Sprite>("Portraits/" + portraitName);
        portraitImage.sprite = portraitIcon;    
    }

    public virtual void Update(){
        if (dialogueRunner.IsDialogueRunning){
            if (Input.GetKeyDown(KeyCode.Return)){
                lineView.UserRequestedViewAdvancement();
            }
        }
        else {
            if (triggerOn && Input.GetKeyDown(KeyCode.E)){
                ActivatePortrait();
                StartConversation();
            }
        }
    }

    public virtual void StartConversation() {
        SFX.Play();
        Debug.Log($"Started conversation with {name}.");
        if (disablePlayerMovement){
            playerController.SetMovement(true);
        }
        isCurrentConversation = true;
        dialogueRunner.StartDialogue(node);
    }

    public void EndConversation() {
        if (isCurrentConversation) {
            isCurrentConversation = false;
            Debug.Log($"Started conversation with {name}.");
            if (disablePlayerMovement){
                playerController.SetMovement(false);
            }
        }
    }

    [YarnCommand("disable")]
    public void DisableConversation() {
        interactable = false;
    }
}