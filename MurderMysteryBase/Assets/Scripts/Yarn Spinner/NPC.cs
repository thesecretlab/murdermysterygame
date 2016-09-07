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
using UnityStandardAssets.Characters.FirstPerson;

namespace Yarn.Unity.Example {
	public class NPC : MonoBehaviour {
        public int objectNumber; 	//the slot number of clue in inventory

		public string characterName = "";

		[FormerlySerializedAs("startNode")]
		public string talkToNode = "";

		[Header("Optional")]
		public TextAsset scriptToLoad;

        public GameObject player;

        public Transform playerPosition;

        public Transform npcPosition;	//Position of the clue object

        public GameObject interactionButton;

        public GameObject inventory;

        public GameObject reticle;

        public DialogueUI dialogueUI;

        public GameTime gameTime;

        public float range = 2;

        public float currentRange = 0;

        public bool mouseIsOver = false;

        public bool isClueObject = false;

        private Color startcolor;	//initial outline color of cube

        // Use this for initialization
        void Start () {
            if (isClueObject) {
                startcolor = GetComponent<Renderer>().material.GetColor("_OutlineColor");
            }
			if (scriptToLoad != null) {
				FindObjectOfType<Yarn.Unity.DialogueRunner>().AddScript(scriptToLoad);
			}
            player = GameObject.FindGameObjectWithTag("Player");
            playerPosition = player.GetComponent<Transform>();
            
            npcPosition = this.GetComponent<Transform>();

            interactionButton = GameObject.FindGameObjectWithTag("InteractionButton");

            inventory = GameObject.FindGameObjectWithTag("Inventory");

            reticle = GameObject.FindGameObjectWithTag("Reticle");

            dialogueUI = GameObject.FindObjectOfType<DialogueUI>();

        }

        // Update is called once per frame
        void Update () {
            if (player == null)
            {
                player = GameObject.FindGameObjectWithTag("Player");
            }
            currentRange = Vector3.Distance(playerPosition.position, npcPosition.position);
            if (dialogueUI.inDialogue)
            {
                if (isClueObject)
                {
                    inventory.GetComponent<HUD>().slots[objectNumber] = true;
                }
                interactionButton.GetComponent<CanvasGroup>().alpha = 0;
                reticle.GetComponent<CanvasGroup>().alpha = 0;
            }
            else if (Vector3.Distance(playerPosition.position, npcPosition.position) < range && mouseIsOver)
            {
                if (isClueObject)
                {
                    GetComponent<Renderer>().material.SetColor("_OutlineColor", Color.yellow);
                    GetComponent<Renderer>().material.SetFloat("_Outline width", 1f);
                }
                interactionButton.GetComponent<CanvasGroup>().alpha = 1;
                reticle.GetComponent<CanvasGroup>().alpha = 0;
            }
            else if (Vector3.Distance(playerPosition.position, npcPosition.position) > range && mouseIsOver)
            {
                if (isClueObject)
                {
                    GetComponent<Renderer>().material.SetColor("_OutlineColor", startcolor);
                    GetComponent<Renderer>().material.SetFloat("_Outline width", 0.002f);
                }
                interactionButton.GetComponent<CanvasGroup>().alpha = 0;
                reticle.GetComponent<CanvasGroup>().alpha = 1;
            }
        }

        void OnMouseEnter()
        {
            Debug.Log("Mouse Enter");
            mouseIsOver = true;
        }

        void OnMouseOver()
        {
            mouseIsOver = true;
        }

        void OnMouseExit()
        {
            Debug.Log("Mouse Exit");
            mouseIsOver = false;
            if (isClueObject)
            {
                GetComponent<Renderer>().material.SetColor("_OutlineColor", startcolor);
            }
            interactionButton.GetComponent<CanvasGroup>().alpha = 0;
            if (!dialogueUI.inDialogue)
            {
                reticle.GetComponent<CanvasGroup>().alpha = 1;
            }
        }

    }

}
