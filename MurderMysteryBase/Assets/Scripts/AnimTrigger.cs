using UnityEngine;
using System.Collections;
using Yarn.Unity;
using Yarn.Unity.Example;

public class AnimTrigger : MonoBehaviour {



	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

			
	}



	[YarnCommand("runAnimation")]
	public void runAnimation(string r, string name)
	{
		if (name == "Nora") {
			
			switch (r) {

			case "angry":
			
				GameObject.Find (name).GetComponent<Animator> ().SetTrigger ("angry");
				break;

			case "happy":
				GameObject.Find (name).GetComponent<Animator> ().SetTrigger ("happy");
				break;


			case "talking":
				GameObject.Find (name).GetComponent<Animator> ().SetTrigger ("talking");
				break;

			case "sad":
				GameObject.Find (name).GetComponent<Animator> ().SetTrigger ("sad");
				break;
			}
		} else if (name == "Jonas") {
			switch (r) {

			case "talking":

				GameObject.Find (name).GetComponent<Animator> ().SetTrigger ("talking");
				break;

			case "talking2":
				GameObject.Find (name).GetComponent<Animator> ().SetTrigger ("talking2");
				break;
			}

		} else if (name == "Logan") {
			switch (r) {

			case "talking":

				GameObject.Find (name).GetComponent<Animator> ().SetTrigger ("talking");
				break;

			case "talking2":
				GameObject.Find (name).GetComponent<Animator> ().SetTrigger ("talking2");
				break;

			case "talking3":
				GameObject.Find (name).GetComponent<Animator> ().SetTrigger ("talking3");
				break;
			}
		}
	}
}
