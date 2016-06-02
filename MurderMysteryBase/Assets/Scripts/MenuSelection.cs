using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuSelection : MonoBehaviour
{
    public bool newGame;
    public bool options;
    public bool quit;
    // Use this for initialization

    void OnMouseUp()
    {
        //Currently both options select the map screen, easy to change.
        if (newGame)
        {
			Debug.Log("Button: New Game");
			SceneManager.LoadScene("detectivesOffice");
        }
       
        if (options)
        {
			Debug.Log("Button: Options");
			SceneManager.LoadScene("murderscene");
			//SceneManager.LoadScene("optionsScene");
        }
        if (quit)
        {
			Debug.Log("Button: Quit");
            Application.Quit();
        }
    }
}