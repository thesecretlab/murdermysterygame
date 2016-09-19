using UnityEngine;
using System.Collections;
using Yarn.Unity;
using Yarn.Unity.Example;

public class NoraEventScript : MonoBehaviour {
    public GameObject playerObject;

    public bool isStart = true;

    public GameObject thisObj;
    public GameObject startPos;

    private CharacterController playerController;
    private LightFlash phoneFlash;
    private NPC phoneDialogue;
    private DialogueRunner dialogueRunner;
    private FadeBetweenScreens fadeController;
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

            dialogueRunner.StartDialogue("Nora.Intro");
            isStart = false;
        }
    }

    [YarnCommand("FadeScreen")]
    public void FadeScreen()
    {
        fadeController.BeginFade(1);
        StartCoroutine("movePlayer");
    }

    public IEnumerator movePlayer()
    {

        yield return new WaitForSeconds(3);
        playerObject.transform.position = startPos.transform.position;
        fadeController.BeginFade(-1);
    }
}
