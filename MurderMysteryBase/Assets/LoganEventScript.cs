using UnityEngine;
using System.Collections;
using Yarn.Unity;
using Yarn.Unity.Example;


public class LoganEventScript : MonoBehaviour {
    public GameObject playerObject;

    public bool isStart = true;
    
    public GameObject thisObj;
    public GameObject logan;
    public GameObject loganPos;

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

    void Update () {
        if (isStart)
        {
            dialogueRunner.StartDialogue("Logan.Intro");
            isStart = false;
        }
	}

    [YarnCommand ("FadeScreen")]
    public void FadeScreen()
    {
        fadeController.BeginFade(1);
        StartCoroutine("moveLogan");
    }

    public IEnumerator moveLogan()
    {

        yield return new WaitForSeconds(3);
        logan.transform.position = loganPos.transform.position;
        fadeController.BeginFade(-1);
    }
}
