using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelLoadDisplay : MonoBehaviour {

    public PlayerPrompts playerPrompt;

    public GameTime gameTime;

    public string sceneName;

	// Use this for initialization
	void Start ()
	{
        playerPrompt = this.GetComponent<PlayerPrompts>();
        gameTime = GameObject.Find("GUI").GetComponent<GameTime>();
	}
	
	// Update is called once per frame
	void Update ()
	{
        if (playerPrompt == null || gameTime == null)
        {
            playerPrompt = this.GetComponent<PlayerPrompts>();
            gameTime = GameObject.Find("GUI").GetComponent<GameTime>();
        }
	}

	void OnLevelWasLoaded()
	{
		sceneName = SceneManager.GetActiveScene().name;
        Debug.Log("Level Loaded "+sceneName);
        
        switch (sceneName)
		{
			case "murderscene":
                playerPrompt.promptPlayer(string.Format("Victim's Apartment {0}", gameTime.getGameTimeString(true)), "3", "3");
                break;
            case "lobby":
                playerPrompt.promptPlayer(string.Format("Victim's Apartment Building {0}", gameTime.getGameTimeString(true)), "3", "3");
                break;
			case "detectivesOffice":
                playerPrompt.promptPlayer(string.Format("Detective's Office {0}", gameTime.getGameTimeString(true)), "3", "3");
                break;
            case "BarLevel":
                playerPrompt.promptPlayer(string.Format("Alchemy {0}", gameTime.getGameTimeString(true)), "3", "3");
                break;
            case "alleyway":
                playerPrompt.promptPlayer(string.Format("Alleyway Behind Alchemy {0}", gameTime.getGameTimeString(true)), "3", "3");
                break;
            case "Morgue":
                playerPrompt.promptPlayer(string.Format("City Morgue {0}", gameTime.getGameTimeString(true)), "3", "3");
                break;
            case "NoraApartment":
                playerPrompt.promptPlayer(string.Format("Nora Hastings Apartment {0}", gameTime.getGameTimeString(true)), "3", "3");
                break;
            case "InterviewRoom":
                playerPrompt.promptPlayer(string.Format("Interview Room {0}", gameTime.getGameTimeString(true)), "3", "3");
                break;
            case "LogansApartment":
                playerPrompt.promptPlayer(string.Format("Logan Rutledge's Apartment {0}", gameTime.getGameTimeString(true)), "3", "3");
                break;
            case "RachaelOffice":
                playerPrompt.promptPlayer(string.Format("Rachael Baxters's Office {0}", gameTime.getGameTimeString(true)), "3", "3");
                break;


		}
    }


}
