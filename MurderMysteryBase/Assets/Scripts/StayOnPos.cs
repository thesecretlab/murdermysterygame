using UnityEngine;
using System.Collections;

public class StayOnPos : MonoBehaviour {

    public Transform target;
    
	void Update () {
        transform.position = target.position;
	}
}
