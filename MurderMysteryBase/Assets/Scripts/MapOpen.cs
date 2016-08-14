using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;
using System.Collections;
using Yarn.Unity;

public class MapOpen : MonoBehaviour
{

    public bool mapKeyEnabled = true;

    public bool ignoreLocation = false;

    public GameObject Player;

    public GameObject MapObject;


    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        MapObject = GameObject.FindGameObjectWithTag("Map");
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M)&& mapKeyEnabled)
        {
            FindObjectOfType<DialogueRunner>().StartDialogue("MapDialogue");
            Player.GetComponent<FirstPersonController>().enabled = false;
        }
    }

    [YarnCommand("loadLevel")]
    public void loadlLevel(string levelName)
    {
        Debug.Log("Loading level");
        Debug.Log(levelName);
        if (levelName == "morgue")
        {
            if (SceneManager.GetActiveScene().name != "morgue"||ignoreLocation==true)
            {
                SceneManager.LoadScene("morgue"/*Morgue scene name goes here*/);
            }
        }
        if (levelName == "office")
        {
            if (SceneManager.GetActiveScene().name != "detectivesOffice" || ignoreLocation == true)
            {
                SceneManager.LoadScene("detectivesOffice");
            }
        }
        if (levelName == "murderscene")
        {
            if (SceneManager.GetActiveScene().name != "murderscene" || ignoreLocation == true)
            {
                SceneManager.LoadScene("murderscene");
            }
        }
        if (levelName == "testscene")
        {
            if (SceneManager.GetActiveScene().name != "Character Test Chamber" || ignoreLocation == true)
            {
                SceneManager.LoadScene("Character Test Chamber");
            }
        }
    }
    

}

