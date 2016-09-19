using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;
using System.Collections;
using Yarn.Unity;

public class MapOpen : MonoBehaviour
{

    public bool mapKeyEnabled = true;

    public bool ignoreLocation = false;

    public GameObject player;

    public GameObject MapObject;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        MapObject = GameObject.FindGameObjectWithTag("Map");

	
    }


    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        if (Input.GetKeyDown(KeyCode.M)&& mapKeyEnabled)
        {
            FindObjectOfType<DialogueRunner>().StartDialogue("MapDialogue");
            player.GetComponent<FirstPersonController>().enabled = false;
        }
        
    }

    [YarnCommand("loadLevel")]
    public void loadlLevel(string levelName)
    {
        Debug.Log("Loading level");
        Debug.Log(levelName);
        if (SceneManager.GetActiveScene().name != levelName || ignoreLocation == true)
        {
            SceneManager.LoadScene(levelName);
        }
    }
    

}

