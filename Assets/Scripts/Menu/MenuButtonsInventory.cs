using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuButtonsInventory : MenuButtons
{
	[SerializeField] PlayerInventory playerInventory;
    //  Select if player inventory is in scene.
    public bool playerItemsInScene;
	public TextMeshProUGUI playerItemTitle;
    public TextMeshProUGUI playerItemText;
	public TextMeshProUGUI questDescriptionText;

    public override void Update(){
    	//  Array of all game objects with tag "MenuButton."
        menuButton = GameObject.FindGameObjectsWithTag("MenuButton");
        if(menuButton.Length > 0){
            //  Check if menu has changed upon first button being activated.
	        if(firstButton!=menuButton[0]){
				//  Remain on current slot if item used on player inventory menu.
				if (playerItemsInScene && playerInventory.GetItemActionStatus()){
					selectedButton = GetLastIndex();
					SetLastIndex(selectedButton);
					playerInventory.SetItemActionStatus(false);
				}
				else {
					selectedButton = 0;
					SetLastIndex(selectedButton);
				}
	        }

	        firstButton = menuButton[0];
	        //  Highlight the active button.
	        for(int a = 0; a < menuButton.Length; a++){
	    		if (a == selectedButton){
	        		menuButton[a].GetComponent<Button>().Select();
                    //  Set and display inventory item description text if item description is active.
                    if (playerItemsInScene && playerItemText.isActiveAndEnabled){
                        TextMeshProUGUI[] textArrayPlayer = menuButton[a].GetComponentsInChildren<TextMeshProUGUI>();
                        if (textArrayPlayer.Length > 0){
							if (itemTextDictPlayer.ContainsKey(textArrayPlayer[1].text)){
								playerItemTitle.text = textArrayPlayer[1].text;
								playerItemText.text = itemTextDictPlayer[textArrayPlayer[1].text];
							}
							else {
								playerItemTitle.text = "";
								playerItemText.text = "";
							}
                        }
                    }
                }
	    		else {
	        	}
	    	}
	        
	        //  Get position of all buttons.
	        Vector3 buttonPositions = menuButton[selectedButton].GetComponent<RectTransform>().position;
	        //  Array for differences in the positions of buttons
	        float[] horizontalDifference  = new float[menuButton.Length];
	        float[] verticalDifference = new float[menuButton.Length];
	        //  Compute the differences in position of buttons
	        for(int a = 0; a<menuButton.Length; a++){
	        	if(a != selectedButton){
	       			Vector3 buttonPosition2 = menuButton[a].GetComponent<RectTransform>().position;
	       			horizontalDifference[a]=buttonPositions.x-buttonPosition2.x;
	       			verticalDifference[a]=buttonPositions.y-buttonPosition2.y;
	       		}
	       	}
	        //  If a closer difference in positions for buttons is true, this will be overwriten.
	        float verticalNew = 9999;
	        float horizontalNew = 9999;

	        if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && !buttonPressed){
	        	menuSFX.Play();
				buttonPressed=true;
	        	for(int a = 0; a < menuButton.Length; a++){
	        		//  Don't test for the selected buttons.
	        		if(a != selectedButton){
	        			//  Check for correct direction.
	        			if(verticalDifference[a]>0){
	        				//  Find closest button in both directions.
	        				if (Mathf.Abs(horizontalDifference[a])<Mathf.Abs(horizontalNew)){
	        					horizontalNew = horizontalDifference[a];
	        					if (Mathf.Abs(verticalDifference[a])<verticalNew){
	        						verticalNew = verticalDifference[a];
	        						selectedButton = a;
	        					}
	        					else {
	        						selectedButton = a;
	        					}
	        				}
	        			}
	        		}
	        	}
                Debug.Log("Selected button is: " + selectedButton);
				SetLastIndex(selectedButton);
	        }
	        
	        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && !buttonPressed){
	        	menuSFX.Play();
				buttonPressed=true;
	        	for(int a = 0; a < menuButton.Length; a++){
	        		if(a != selectedButton){
	        			if(verticalDifference[a]<0){
	        				if(Mathf.Abs(horizontalDifference[a])<=Mathf.Abs(horizontalNew)){
	        					horizontalNew = horizontalDifference[a];
	        					if(Mathf.Abs(verticalDifference[a])<=verticalNew){
	        						verticalNew = verticalDifference[a];
	        						selectedButton = a;
	        					}
	        					else{
	        						selectedButton = a;
	        					}
	        				}
	        			}
	        		}
	        	}
                Debug.Log("Selected button is: " + selectedButton);
				SetLastIndex(selectedButton);
	        }
	        
	        if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && !buttonPressed){
	        	menuSFX.Play();
				buttonPressed=true;
	        	for(int a = 0; a < menuButton.Length; a++){
	        		if(a != selectedButton){
	        			if(horizontalDifference[a]>0){
	        				if(Mathf.Abs(verticalDifference[a])<Mathf.Abs(verticalNew)){
	        					verticalNew = verticalDifference[a];
	        					if(Mathf.Abs(horizontalDifference[a])<horizontalNew){
	        						horizontalNew = horizontalDifference[a];
	        						selectedButton = a;
	        					}
	        					else{
	        						selectedButton = a;
	        					}
	        				}
	        			}
	        		}
	        	}
                Debug.Log("Selected button is: " + selectedButton);
				SetLastIndex(selectedButton);
	        }
	        
	        if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && !buttonPressed){
	        	menuSFX.Play();
				buttonPressed=true;
	        	for(int a = 0; a < menuButton.Length; a++){
	        		if(a != selectedButton){
	        			if(horizontalDifference[a]<0){
	        				if(Mathf.Abs(verticalDifference[a])<Mathf.Abs(verticalNew)){
	        					verticalNew = verticalDifference[a];
	        					if(Mathf.Abs(horizontalDifference[a])<horizontalNew){
	        						horizontalNew = horizontalDifference[a];
	        						selectedButton = a;
	        					}
	        					else{
	        						selectedButton = a;
	        					}
	        				}
	        			}
	        		}
	        	}
                Debug.Log("Selected button is: " + selectedButton);
				SetLastIndex(selectedButton);
	        }
	        if (horizontal==0 && vertical==0){buttonPressed=false;} 
        }
        else {
			selectedButton = 0;
			SetLastIndex(selectedButton);
			}
    }
}
