using UnityEngine;
using System.Collections;

public class DoorCode : MonoBehaviour {

	public bool mouseIsOver = false;

	void OnMouseEnter()
	{
		mouseIsOver = true;
		//Debug.Log("DoorOver");
	}

	void OnMouseExit()
	{
		mouseIsOver = false;
		//Debug.Log("DoorNotOver");
	}
}
