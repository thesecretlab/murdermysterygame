using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Yarn.Unity;
using Yarn.Unity.Example;

//Sets up the camera for the Intro scene

public class IntroSceneSetup : MonoBehaviour
{

    public GameObject playerObject;
    public GameObject phoneLight;
    public GameObject phoneDialogueObj;
    public GameObject chair;
    public GameObject lookatPosition;

    public bool isGameStart = true;
    public bool isInChair = false;
    public bool isPhoneRinging = false;
    public bool isSceneDone = false;
    public GameObject thisObj;

    private CharacterController playerController;
    private LightFlash phoneFlash;
    private NPC phoneDialogue;
    private int tick = 0;
    private DialogueRunner dialogueRunner;
    private FadeBetweenScreens fadeController;
    private VariableStorage yarnVars;


    void Start()
    {
        dialogueRunner = GameObject.Find("DialogueObject").GetComponentInChildren<DialogueRunner>();
        yarnVars = GameObject.Find("DialogueObject").GetComponentInChildren<VariableStorage>();
        playerController = playerObject.GetComponent<CharacterController>();
        phoneFlash = phoneLight.GetComponent<LightFlash>();
        phoneDialogue = phoneDialogueObj.GetComponent<NPC>();
        fadeController = GameObject.Find("FadeController").GetComponent<FadeBetweenScreens>();
        thisObj = this.gameObject;
    }

    void Update()
    {
        if (!(yarnVars.GetValue("NewGameIntroDone").AsBool))
        {
            MakePhoneRing();
            if (isGameStart)
            {
                MovePlayerToChair();
                phoneDialogue.enabled = false;
                isGameStart = false;
                isInChair = true;
                dialogueRunner.StartDialogue("Detective.Intro.Monologue");
            }
            if (isSceneDone)
            {
                isInChair = false;
                playerController.enabled = true;
                fadeController.fadeSpeed = 0.25f;
                
            }
        }
    }

    public void MovePlayerToChair()
    {
        playerObject.transform.position = chair.transform.position;
        
        playerController.enabled = false;
    }

    public void MakePhoneRing()
    {
        phoneFlash.isRinging = isPhoneRinging;
        phoneDialogue.enabled = isPhoneRinging;
    }

    [YarnCommand("startPhoneRing")]
    public void StartPhoneRing()
    {
        Debug.Log("RINGRING");
        GameObject.Find("IntroEventSystem").GetComponent<IntroSceneSetup>().isPhoneRinging = true;
    }

    [YarnCommand("stopPhoneRing")]
    public void StopPhoneRing()
    {
        GameObject.Find("IntroEventSystem").GetComponent<IntroSceneSetup>().isPhoneRinging = false;
    }

    [YarnCommand("UnlockPlayer")]
    public void UnlockPlayer()
    {
        isSceneDone = true;
        playerObject.transform.position = lookatPosition.transform.position;
    }
}
