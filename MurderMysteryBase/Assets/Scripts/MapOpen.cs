using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;
using System.Collections;
using Yarn.Unity;


//!  Map Open Class
/*!
 * Controls the 'Map', the dialogue used for traveling between scenes, and handles the scene switches and game exits.
*/

namespace Yarn.Unity.GameScripts
{
    public class MapOpen : MonoBehaviour
    {

        public bool mapKeyEnabled = true;

        public bool ignoreLocation = false;

        public GameObject player;

        public GameObject MapObject;

        public GameTime gameTime;

        public DialogueUI dialogueUI;


        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            MapObject = GameObject.FindGameObjectWithTag("Map");
            gameTime = GameObject.FindObjectOfType<GameTime>();

            dialogueUI = GameObject.FindObjectOfType<DialogueUI>();
        }


        void Update()
        {
            if (player == null || MapObject == null || dialogueUI == null || gameTime == null)
            {
                player = GameObject.FindGameObjectWithTag("Player");
                MapObject = GameObject.FindGameObjectWithTag("Map");
                
                dialogueUI = GameObject.FindObjectOfType<DialogueUI>();

                gameTime = GameObject.FindObjectOfType<GameTime>();
            }
            if (Input.GetKeyDown(KeyCode.M) && mapKeyEnabled && !dialogueUI.inDialogue)
            {
                FindObjectOfType<DialogueRunner>().StartDialogue("MapDialogue." + SceneManager.GetActiveScene().name);
            }
            if (Input.GetKey(KeyCode.Escape))
            {
                FindObjectOfType<DialogueRunner>().StartDialogue("Exit.Exit");
            }
            if (gameTime.seconds > 93600 && !dialogueUI.inDialogue)
            {
                FindObjectOfType<DialogueRunner>().StartDialogue("Game.Fail");
            }
        
        }

        /*! This Yarn Spinner Accessible function accepts a string variables, 'levelName', and loads the appropriate scene.
         *  'levelName' is the name of the scene to load.*/
        [YarnCommand("loadLevel")]
        public void loadlLevel(string levelName)
        {
            Debug.Log("Loading level");
            Debug.Log(levelName);
            if (SceneManager.GetActiveScene().name != levelName || ignoreLocation == true)
            {
                gameTime.addGameTime(0, 15);
                SceneManager.LoadScene(levelName);
            }
        }

        /*! This Yarn Spinner Accessible function prompts the player for confirmation before exiting the game.*/
        [YarnCommand("exitGame")]
        public void exitGame()
        {
            Debug.Log("Exiting Game");
            try
            {
                //UnityEditor.EditorApplication.isPlaying = false;
            }
            finally
            {
                Application.Quit();
            }
        }


    }
}
