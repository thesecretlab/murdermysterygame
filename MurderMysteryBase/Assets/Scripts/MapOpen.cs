using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;
using System.Collections;
using Yarn.Unity;

namespace Yarn.Unity.Example
{
    public class MapOpen : MonoBehaviour
    {

        public bool mapKeyEnabled = true;

        public bool ignoreLocation = false;

        public GameObject player;

        public GameObject MapObject;

        public DialogueUI dialogueUI;


        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            MapObject = GameObject.FindGameObjectWithTag("Map");

            dialogueUI = GameObject.FindObjectOfType<DialogueUI>();
        }


        void Update()
        {
            if (player == null)
            {
                player = GameObject.FindGameObjectWithTag("Player");
            }
            if (Input.GetKeyDown(KeyCode.M) && mapKeyEnabled && !dialogueUI.inDialogue)
            {
                FindObjectOfType<DialogueRunner>().StartDialogue("MapDialogue." + SceneManager.GetActiveScene().name);
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
}
