using UnityEngine;
using System.Collections;
using Yarn.Unity;

public class PlayerPrompts : MonoBehaviour {

    public GameTime gameTime;

    public CanvasGroup promptGroup;

	// Use this for initialization
	void Start () {
        gameTime = GameObject.FindObjectOfType<GameTime>();
        promptGroup = this.GetComponent<CanvasGroup>();
	}
	
	// Update is called once per frame
	void Update () {

        /*if(this.GetComponent<CanvasGroup>().alpha > 0)
        {
            this.GetComponent<CanvasGroup>().alpha -= 0.005f;
        }*/
	}

    IEnumerator fadePrompt(float displayTime, float fadeTime)
    {
        Debug.Log("Fading Prompt");
        promptGroup.alpha = 1;
        yield return new WaitForSeconds(displayTime);

        for (float f = fadeTime; f >= 0; f -= fadeTime / 100)
        {
            promptGroup.alpha -= fadeTime / 100;
            yield return new WaitForSeconds(fadeTime / 100);
        }
    }

    [YarnCommand("promptPlayer")]
    public void promptPlayer(string prompt,string displayTime, string fadeTime)
    {
        this.GetComponent<UnityEngine.UI.Text>().text = prompt;
        //promptGroup.alpha = 1;

        int parsedDisplayTime = 0;
        int parsedFadeTime = 0;
        if (int.TryParse(displayTime, out parsedDisplayTime))
        {
            if (int.TryParse(fadeTime, out parsedFadeTime))
            {
                StartCoroutine(fadePrompt(parsedDisplayTime, parsedFadeTime));
            }
            else
            {
                Debug.Log("Invalid Display Time");
            }
        }
        else
        {
            Debug.Log("Invalid Display Time");
        }
    }

}
