using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MapSelection : MonoBehaviour {

    public bool morgue;
    public bool office;
    public bool murderScene;

    public GameObject map;

    void Start()
    {
        map = GameObject.FindGameObjectWithTag("Map");
    }


    void OnMouseUp()
    {
        //to do: scene selection and  maybe game timer?
        if (morgue)
        {
            map.GetComponent<MapOpen>().cameraSwitch();
            if (SceneManager.GetActiveScene().name != "morgue")
            {
                SceneManager.LoadScene("morgue"/*Morgue scene name goes here*/);
            }
        }
        if (office)
        {
            map.GetComponent<MapOpen>().cameraSwitch();
            if (SceneManager.GetActiveScene().name != "detectivesOffice")
            {
                SceneManager.LoadScene("detectivesOffice");
            }
        }
        if (murderScene)
        {
            map.GetComponent<MapOpen>().cameraSwitch();
            if (SceneManager.GetActiveScene().name != "murderscene")
            {
                SceneManager.LoadScene("murderscene");
            }
        }
    }

}

