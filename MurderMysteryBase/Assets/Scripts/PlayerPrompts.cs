using UnityEngine;
using System.Collections;
using Yarn.Unity;

public class PlayerPrompts : MonoBehaviour {

    public GameTime gameTime;

	// Use this for initialization
	void Start () {
        gameTime = GameObject.FindObjectOfType<GameTime>();
	}
	
	// Update is called once per frame
	void Update () {

        if(this.GetComponent<CanvasGroup>().alpha > 0)
        {
            this.GetComponent<CanvasGroup>().alpha -= 0.005f;
        }
	}

    [YarnCommand("promptPlayer")]
    public void promptPlayer(string prompt)
    {
        this.GetComponent<UnityEngine.UI.Text>().text = prompt;
        this.GetComponent<CanvasGroup>().alpha = 1;
    }

}
