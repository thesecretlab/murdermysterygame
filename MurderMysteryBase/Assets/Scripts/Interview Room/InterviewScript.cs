using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;
using System.Collections;
using Yarn.Unity;

//!  Interview Room Setup Class
/*!
 Sets up the Interview room ready to interrogate a suspect.
*/

namespace Yarn.Unity.GameScripts
{
    public class InterviewScript : MonoBehaviour
    {

        public GameObject Logan;
        public GameObject Nora;
        public GameObject Rachael;
        public GameObject Lola;

        public GameObject suspect;

        public GameObject player;

        public GameObject chair;

        private DialogueRunner dialogueRunner;

        // Use this for initialization
        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");

            chair = GameObject.Find("PRE_FUR_Kitchen_chair_01_01 (1)");

            dialogueRunner = GameObject.Find("DialogueObject").GetComponentInChildren<DialogueRunner>();

            Logan = GameObject.Find("Logan");
            Nora = GameObject.Find("Nora");
            Rachael = GameObject.Find("Rachael");
            Lola = GameObject.Find("Lola");

            dialogueRunner.StartDialogue("Interview.Setup");
        }

        // Update is called once per frame
        void Update()
        {

        }

        void Awake()
        {
            
        }

        /*! This Yarn Spinner Accessible function accepts string 'suspectName' and activates the appropriate character in the scene.*/
        [YarnCommand("interviewSuspect")]
        public void interview(string suspectName)
        {
            Debug.Log("Loading interrogation of " + suspectName);
            suspect = GameObject.Find(suspectName);

            Logan.SetActive(false);
            Nora.SetActive(false);
            Rachael.SetActive(false);
            Lola.SetActive(false);
            suspect.SetActive(true);

        }

        /*! This Yarn Spinner Accessible function moves the players viewpoint t a sitting position on the chair in the interrogation room.*/
        [YarnCommand("sit")]
        public void sit()
        {
            Vector3 position = chair.transform.position;
            position.y = 0.5f;
            player.transform.position = position;

            player.GetComponent<CharacterController>().enabled = false;
        }
    }
}