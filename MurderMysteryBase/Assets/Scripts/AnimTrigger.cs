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
	public void runAnimation(string r)
	{

		switch (r) {

		case "angry":
			
			GameObject.Find("Nora").GetComponent<Animator>().SetTrigger("angry");

			break;

		case "happy":
			GameObject.Find("Nora").GetComponent<Animator>().SetTrigger("happy");

			break;


		case "talking":
			GameObject.Find("Nora").GetComponent<Animator>().SetTrigger("talking");

			break;

		case "sad":
			GameObject.Find ("Nora").GetComponent<Animator> ().SetTrigger ("sad");

			break;
		}
	}
}
