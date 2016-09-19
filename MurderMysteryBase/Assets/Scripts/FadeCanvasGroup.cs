using UnityEngine;
using System.Collections;

public class FadeCanvasGroup : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void fadeIn(float fadeTime){
        StartCoroutine(fadeInGroup(fadeTime));
    }

    public void fadeOut(float fadeTime)
    {
        StartCoroutine(fadeOutGroup(fadeTime));
    }

    IEnumerator fadeInGroup(float fadeTime)
    {
        Debug.Log("Fading In Group");
        this.GetComponent<CanvasGroup>().alpha = 0;

        for (float f = fadeTime; f >= 0; f -= fadeTime / 100)
        {
            this.GetComponent<CanvasGroup>().alpha += fadeTime / 100;
            yield return new WaitForSeconds(fadeTime / 100);
        }
    }

    IEnumerator fadeOutGroup(float fadeTime)
    {
        Debug.Log("Fading Out Group");
        this.GetComponent<CanvasGroup>().alpha = 1;

        for (float f = fadeTime; f >= 0; f -= fadeTime / 100)
        {
            this.GetComponent<CanvasGroup>().alpha -= fadeTime / 100;
            yield return new WaitForSeconds(fadeTime / 100);
        }
    }
}
