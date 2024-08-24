using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour, IDataPersistence
{
    public float movementSpeed = 5f;
    public Rigidbody2D rigidbody;
    public Animator animator;
    Vector2 playerMovement;
    // [SerializeField] InGameMenu gameMenu; // use with: if(!gameMenu.GamePaused() && !movementDisabled)
    private bool movementDisabled = false;
    Vector3 defaultInitialPosition = Vector3.zero;

    //  Save player position in game data.
    public void SaveData(GameData data){
        data.playerPosition = this.transform.position;
    }

    //  Load player position from game data.
    public void LoadData(GameData data){
        //  If last saved scene matches the current scene, load player position.
        if (SceneManager.GetActiveScene().name == data.currentSceneName){
            this.transform.position = data.playerPosition;
        }
        //  If last saved scene doesn't match the current scene, set player position to new scene default.
        else {
            this.transform.position = data.nextSceneInitialPos;
        }
    }

    void Update() {
        if (!movementDisabled){
            playerMovement.x = Input.GetAxisRaw("Horizontal");
            playerMovement.y = Input.GetAxisRaw("Vertical");
            //  Disable diagonal movement.
            if (playerMovement.x != 0) playerMovement.y = 0;
            //  Apply animation.
            animator.SetFloat("Horizontal", playerMovement.x);
            animator.SetFloat("Vertical", playerMovement.y);
            animator.SetFloat("Speed", playerMovement.sqrMagnitude);

            //  Player to face last direction.
            if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1){
                animator.SetFloat("lastMoveX", Input.GetAxisRaw("Horizontal")); 
                animator.SetFloat("lastMoveY", Input.GetAxisRaw("Vertical"));  
            }
        }
    }

    void FixedUpdate() {
        rigidbody.MovePosition(rigidbody.position + playerMovement * movementSpeed * Time.fixedDeltaTime);
    }

    public void SetMovement(bool updateMovement){
        movementDisabled = updateMovement;
    }

    public void PlayerDeath(){
        Debug.Log("Player Death ().");
        SetMovement(true); 
        GameEventsManager.instance.PlayerDeath();
        // yield return new WaitForSeconds(0.4f);
        PlayerReset();
    }

    public void PlayerReset(){
        SetMovement(false); 
        this.transform.position = defaultInitialPosition;
        Debug.Log("Player Reset ().");
    }
}