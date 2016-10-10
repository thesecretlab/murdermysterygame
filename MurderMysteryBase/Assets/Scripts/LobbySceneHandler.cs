using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Yarn.Unity;
using Yarn.Unity.GameScripts;
using UnityStandardAssets.Characters.FirstPerson;

public class LobbySceneHandler : MonoBehaviour {


    public bool sceneStart;

    private GameObject player;
    private GameObject officer1;
    private DialogueRunner dialogueRunner;

	void Start () {
        dialogueRunner = GameObject.Find("DialogueObject").GetComponentInChildren<DialogueRunner>();
        player = GameObject.Find("FPSController");
        officer1 = GameObject.Find("Officer_Uniformed_2");
        StartCoroutine(dialogueCheck());
    }

    IEnumerator dialogueCheck()
    {
        player.GetComponent<FirstPersonController>().LockControllerReleaseMouse(true);
        yield return new WaitForSeconds(.5f);
        if (sceneStart)
        {
            officer1.GetComponent<Animator>().SetBool("IsTalking", true);
            dialogueRunner.StartDialogue("Officer1.Intro");
            sceneStart = false;
        }
    }

    [YarnCommand("SetAnimParameterBool")]
    public void StartPhoneRing(string character, string param, string tf)
    {
        bool trufalse;
        GameObject Character = GameObject.Find(character);
        if (tf == "1")
        {
            trufalse = true;
        }
        else
        {
            trufalse = false;
        }
        Character.GetComponent<Animator>().SetBool(param, trufalse);
    }
}
