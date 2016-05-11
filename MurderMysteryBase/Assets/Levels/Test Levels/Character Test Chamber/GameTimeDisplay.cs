using UnityEngine;
using System.Collections;

public class GameTimeDisplay : MonoBehaviour {

	// Use this for initialization
	void Start () {
        this.GetComponent<UnityEngine.UI.Text>().text = "00:00:00";
	}

	// Update is called once per frame
	void Update () {
        this.GetComponent<UnityEngine.UI.Text>().text = GameObject.Find("GameTimeObject").GetComponent<GameTime>().getGameTimeString(); 

    }
}
