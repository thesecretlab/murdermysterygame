using UnityEngine;
using System.Collections;

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
