/*

The MIT License (MIT)

Copyright (c) 2015 Secret Lab Pty. Ltd. and Yarn Spinner contributors.

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

*/

using UnityEngine;
using System.Collections;
using UnityEngine.Serialization;

namespace Yarn.Unity.Example {
	public class NPC : MonoBehaviour {

		public string characterName = "";

		[FormerlySerializedAs("startNode")]
		public string talkToNode = "";

		[Header("Optional")]
		public TextAsset scriptToLoad;

        public Transform playerPosition;

        public Transform npcPosition;	//Position of the clue object

        public Canvas interaction; //A reference to the Canvas UI Object

        public DialogueUI dialogueUI;

        public float range = 2;

        public bool mouseIsOver = false;

        // Use this for initialization
        void Start () {
			if (scriptToLoad != null) {
				FindObjectOfType<Yarn.Unity.DialogueRunner>().AddScript(scriptToLoad);
			}
            playerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            npcPosition = this.GetComponent<Transform>();
            interaction = GameObject.FindGameObjectWithTag("Interaction").GetComponent<Canvas>();
            //dialogueUI = GameObject.FindGameObjectWithTag("Dialogue").GetComponent<DialogueUI>();
            dialogueUI = GameObject.FindObjectOfType<DialogueUI>();

        }

        // Update is called once per frame
        void Update () {
            if (dialogueUI.inDialogue)
            {
                interaction.enabled = false;
            }
            else if (Vector3.Distance(playerPosition.position, npcPosition.position) < range && mouseIsOver)
            {
                interaction.enabled = true;
            }
            else if (Vector3.Distance(playerPosition.position, npcPosition.position) > range && mouseIsOver)
            {
                interaction.enabled = false;
            }
        }

        void OnMouseEnter()
        {
            mouseIsOver = true;
        }

        void OnMouseExit()
        {
            mouseIsOver = false;
            interaction.enabled = false;
        }
    }

}
