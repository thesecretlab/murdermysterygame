using UnityEngine;
using System.Collections;

//!  Integrates objects with the inventory (Not Implemented In Current Release)
/*!
 This class handles adding Clue Objects to the player's inventory.
*/


public class ClueObject : MonoBehaviour {
	
	public GameObject Player;
	public GameObject clueObject;
	public int objectNumber;
	

void OnTriggerEnter(Collider col) 
	{ 

		if (col.gameObject.tag == "Player")
		{ 
			clueObject.SetActive(false);
			Player.GetComponent<HUD>().slots[objectNumber] = true;	
		} 
	}
}
