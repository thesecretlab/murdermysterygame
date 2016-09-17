using UnityEngine;
using System.Collections;
using Yarn.Unity;
using Yarn.Unity.Example;

public class CallEventScript : MonoBehaviour {

    public bool callDone = false;

    private DialogueRunner dialogueRunner;

	public void startCall()
    {
        callDone = true;
    }

    void Start()
    {
        dialogueRunner = GameObject.Find("DialogueObject").GetComponentInChildren<DialogueRunner>();
    }

    void Update()
    {
        if (callDone)
        {
            callDone = false;
            dialogueRunner.StartDialogue("MorticianCall.Call");
        }
    }
	
}
