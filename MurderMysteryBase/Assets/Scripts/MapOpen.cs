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
            if (player == null || MapObject== null || dialogueUI == null)
            {
                player = GameObject.FindGameObjectWithTag("Player");
                MapObject = GameObject.FindGameObjectWithTag("Map");
                
                dialogueUI = GameObject.FindObjectOfType<DialogueUI>();
            }
            if (Input.GetKeyDown(KeyCode.M) && mapKeyEnabled && !dialogueUI.inDialogue)
            {
                FindObjectOfType<DialogueRunner>().StartDialogue("MapDialogue." + SceneManager.GetActiveScene().name);
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                FindObjectOfType<DialogueRunner>().StartDialogue("Exit.Exit");
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

        [YarnCommand("exitGame")]
        public void exitGame()
        {
            Debug.Log("Exiting Game");
            Application.Quit();
            //UnityEditor.EditorApplication.isPlaying = false;
        }


    }
}
