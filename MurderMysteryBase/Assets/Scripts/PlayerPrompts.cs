using UnityEngine;
using System.Collections;
using Yarn.Unity;

//!  Player Prompt Class
/*!
 Controls the on screen text prompts to the player.
*/

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

    /*! This Yarn Spinner Accessible function accepts three string variables, 'prompt', 'displayTime', and 'fadeTime' , and prompts the player with on screen text accordingly
     *  'prompt' is the text to display to the player, 'displayTime' is the time in seconds to display the prompt, and 'fadeTime' is the time in seconds over which to fade the prompt out.*/

    [YarnCommand("promptPlayer")]
    public void promptPlayer(string prompt,string displayTime, string fadeTime)
    {
        Debug.Log("Prompting Player");
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
