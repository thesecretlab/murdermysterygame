using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelLoadDisplay : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
		

	}
	
	// Update is called once per frame
	void Update ()
	{
		if(this.GetComponent<UnityEngine.UI.Text>().color.a > 0.0f)
        {
            Color textColor = this.GetComponent<UnityEngine.UI.Text>().color;
            textColor.a -= 0.005f;
            this.GetComponent<UnityEngine.UI.Text>().color = textColor;
        }
	}

	void OnLevelWasLoaded()
	{
		string sceneName = SceneManager.GetActiveScene().name;
		string gameTimeString = GameObject.Find("HUD").GetComponent<GameTime>().getGameTimeString(true);
        Color textColor = this.GetComponent<UnityEngine.UI.Text>().color;
        textColor.a = 1f;
        
        switch (sceneName)
		{
			case "murderscene":
				this.GetComponent<UnityEngine.UI.Text>().text = string.Format("Victim's Apartment {0}", gameTimeString);
                break;
			case "detectivesOffice":
				this.GetComponent<UnityEngine.UI.Text>().text = string.Format("Detective's Office {0}", gameTimeString);
                break;
		}
        this.GetComponent<UnityEngine.UI.Text>().color = textColor;
    }


}
