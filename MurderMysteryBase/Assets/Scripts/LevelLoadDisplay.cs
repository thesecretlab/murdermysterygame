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
		
	}

	void OnLevelWasLoaded()
	{
		string sceneName = SceneManager.GetActiveScene().name;
		string gameTimeString = GameObject.Find("HUD").GetComponent<GameTime>().getGameTimeString(true);

		switch (sceneName)
		{
			case "murderscene":
				this.GetComponent<UnityEngine.UI.Text>().text = string.Format("Victim's Apartment {0}", gameTimeString);
				break;
			case "detectivesOffice":
				this.GetComponent<UnityEngine.UI.Text>().text = string.Format("Detective's Office {0}", gameTimeString);
				break;
		}
		Color textColor = this.GetComponent<UnityEngine.UI.Text>().color;
		textColor.a = 1.0f;
		this.GetComponent<UnityEngine.UI.Text>().color = textColor;
		this.GetComponent<UnityEngine.UI.Text>().CrossFadeAlpha(0.0f, 5.0f, false);
	}
}
