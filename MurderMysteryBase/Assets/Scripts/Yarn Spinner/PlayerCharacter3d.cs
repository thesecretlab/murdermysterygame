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
using System.Collections.Generic;
using UnityStandardAssets.Characters.FirstPerson;


namespace Yarn.Unity.GameScripts {
	public class PlayerCharacter3d : MonoBehaviour {


		public float interactionRadius = 2.0f;

        public GameObject Player;

        public GameObject reticle;

        void Start()
        {
            Player = GameObject.FindGameObjectWithTag("Player");
            reticle = GameObject.FindGameObjectWithTag("Reticle");
        }

        // Update is called once per frame
        void Update () {

			// Remove all player control when we're in dialogue
			if (FindObjectOfType<DialogueRunner>().isDialogueRunning == true) {
				return;
			}

			// Detect if we want to start a conversation
			if (Input.GetKeyDown(KeyCode.E)) {
                Debug.Log("Pressed E");
				CheckForNearbyNPC();
                //transform.position = playerController.transform.position;
            }

		}
		public void CheckForNearbyNPC ()
		{
            Debug.Log("Checking for NPC/Clue");
			// Find all DialogueParticipants, and filter them to
			// those that have a Yarn start node and are in range; 
			// then start a conversation with the first one
			var allParticipants = new List<NPC> (FindObjectsOfType<NPC> ());
            var target = allParticipants.Find(delegate(NPC p)
            {
                return string.IsNullOrEmpty(p.talkToNode) == false && // has a conversation node?
                (p.transform.position - this.transform.position)// is in range?
                .magnitude <= interactionRadius && p.mouseIsOver;
            });
			if (target != null) {
                // Kick off the dialogue at this node.
                Debug.Log("Found NPC/Clue: " + target.name);
                Debug.Log("Starting NPC/Clue Dialogue");
                FindObjectOfType<DialogueRunner>().StartDialogue(target.talkToNode);
                Player.GetComponent<FirstPersonController>().LockControllerReleaseMouse(true);
			}
		}
    }

}
