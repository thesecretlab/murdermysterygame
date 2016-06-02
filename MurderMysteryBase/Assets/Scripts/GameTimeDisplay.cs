using UnityEngine;
using System.Collections;

public class GameTimeDisplay : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		this.GetComponent<UnityEngine.UI.Text>().text = GameObject.Find("HUD").GetComponent<GameTime>().getGameTimeString(false);
    }
}
