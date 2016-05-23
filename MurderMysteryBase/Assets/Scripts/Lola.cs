using UnityEngine;
using System.Collections;

public class Lola : MonoBehaviour {

	Animator anim;

    int dance = 0;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.L)&&dance==0)
		{
            dance = 1;
        }
        if (Input.GetKeyDown(KeyCode.O) && dance == 1)
        {
            dance = 2;
        }
        if (Input.GetKeyDown(KeyCode.L) && dance == 2)
        {
            dance = 3;
        }
        if (Input.GetKeyDown(KeyCode.A) && dance == 3)
        {
            dance = 0;
            anim.SetTrigger(Animator.StringToHash("Dance"));
        }
    }
}
