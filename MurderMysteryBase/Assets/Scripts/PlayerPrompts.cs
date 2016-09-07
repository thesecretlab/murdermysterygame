using UnityEngine;
using System.Collections;

public class PlayerPrompts : MonoBehaviour {

    public GameTime gameTime;

	// Use this for initialization
	void Start () {
        gameTime = GameObject.FindObjectOfType<GameTime>();
	}
	
	// Update is called once per frame
	void Update () {
        if (gameTime.totalGameSeconds > 28810 && gameTime.totalGameSeconds < 28811)
        {
            this.GetComponent<UnityEngine.UI.Text>().text = "Press 'm' to access the map dialogue";
            this.GetComponent<CanvasGroup>().alpha = 1;
        }

        if(this.GetComponent<CanvasGroup>().alpha > 0)
        {
            this.GetComponent<CanvasGroup>().alpha -= 0.005f;
        }
	}

}
