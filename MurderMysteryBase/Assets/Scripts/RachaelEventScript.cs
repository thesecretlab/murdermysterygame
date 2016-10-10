using UnityEngine;
using System.Collections;
using Yarn.Unity;
using Yarn.Unity.GameScripts;

//!  Rachael's Office Scene Setup Class
/*!
 Sets up the intro events in the Rachael's Office scene.
*/

public class RachaelEventScript : MonoBehaviour {
    public GameObject playerObject;

    public bool isStart = true;

    public GameObject thisObj;

    private CharacterController playerController;
    private LightFlash phoneFlash;
    private NPC phoneDialogue;
    private DialogueRunner dialogueRunner;
    public FadeBetweenScreens fadeController;
    private VariableStorage yarnVars;


    void Start()
    {
        dialogueRunner = GameObject.Find("DialogueObject").GetComponentInChildren<DialogueRunner>();
        yarnVars = GameObject.Find("DialogueObject").GetComponentInChildren<VariableStorage>();
        playerController = playerObject.GetComponent<CharacterController>();
        fadeController = GameObject.Find("FadeController").GetComponent<FadeBetweenScreens>();
        thisObj = this.gameObject;
    }

    void Update()
    {
        if (isStart)
        {
            
            dialogueRunner.StartDialogue("Rachael.Intro");
            isStart = false;
        }
    }
    
}
