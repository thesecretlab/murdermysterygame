using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MapSelection : MonoBehaviour {

    public bool morgue;
    public bool office;
    public bool murderScene;

    void OnMouseUp()
    {
        //to do: scene selection and  maybe game timer?
        if (morgue)
        {
			SceneManager.LoadScene(1/*Morgue scene number goes here*/);
        }
        if (office)
        {
			SceneManager.LoadScene("detectivesOffice");
        }
        if (murderScene)
        {
			SceneManager.LoadScene("murderscene");
        }
    }
}

