using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

//!  Inventory Control Class (Not Implemented In Current Release)
/*!
 This class controls the in-game Inventory
*/

public class HUD : MonoBehaviour {

	public GameObject inventory; //A reference to the Canvas UI Object
	public bool[] slots = new bool[3]; //An array of 4 slots for the inventory
	//public Camera UIcam;
	public FirstPersonController firstPersonController;
	public Image button1; //A Reference the original button image
	public Image button2;
	public Image button3;
	public Image button4;
	public Sprite clue1; //image of the clue objects
	public Sprite clue2;
	public Sprite clue3;
	public Sprite clue4;
    public bool inventoryEnabled = false;

	// Use this for initialization
	void Start () 
	{
		firstPersonController = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>(); //Gets controller for the player and assigns it to a local variable for ease of use
    	//inventory.enabled = false; //Set the inventory to false at start of scene		
		updateInventory();	
	}
	
	// Update is called once per frame
	void Update () 
	{
		updateInventory(); //call to update the inventory slots	

		if(Input.GetKeyDown("i")) //if player presses inventory hotkey
		{
	 		GameObject myEventSystem = GameObject.Find("EventSystem");
	 		myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
			
			if(inventoryEnabled == false)
			{
				firstPersonController.LockControllerReleaseMouse(true); //locks mouse to controller
				inventoryEnabled = true;
                inventory.GetComponent<CanvasGroup>().alpha = 1;
			}
			else if(inventoryEnabled == true)
			{
                inventoryEnabled = false;
				firstPersonController.LockControllerReleaseMouse(false); //releases mouse cursor
                inventory.GetComponent<CanvasGroup>().alpha = 0;
			}
		}
	}

	void updateInventory()
	{
		for(int i =0; i< slots.Length; i++)
		{	
			if(slots[i] == true)
			{
				updateSlot(i);
			}
		}
	}
	
	void updateSlot(int pos)
	{
		switch(pos){
			case 0:
			button1.overrideSprite = clue1;
			break;
			case 1:
			button2.overrideSprite = clue2;
			break;
			case 2:
			button3.overrideSprite = clue3;
			break;
			case 3:
			button4.overrideSprite = clue4;
			break;
		}
	}
}