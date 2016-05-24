using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MapSelection : MonoBehaviour {

    public bool morgue;
    public bool office;
    public bool murderScene;

    public GameObject map;

	private Color startcolor;	//initial outline color of cube

    void Start()
    {
        map = GameObject.FindGameObjectWithTag("Map");
		startcolor = GetComponent<Renderer>().material.GetColor("_OutlineColor");
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

	void OnMouseEnter()
	{
		GetComponent<Renderer>().material.SetColor("_OutlineColor", Color.yellow);
		GetComponent<Renderer>().material.SetFloat("_Outline width", 1f);
	}

	void OnMouseExit()
	{
		GetComponent<Renderer>().material.SetColor("_OutlineColor", startcolor);
		GetComponent<Renderer>().material.SetFloat("_Outline width", 0.002f);
	}

}

