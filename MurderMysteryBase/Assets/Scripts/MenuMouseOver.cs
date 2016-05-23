using UnityEngine;
using System.Collections;

public class MenuMouseOver : MonoBehaviour {

    void Start()
    {
        GetComponent<Renderer>().material.color = Color.white;
    }

    void OnMouseEnter()
    {
        GetComponent<Renderer>().material.color = Color.grey;
    }

    void OnMouseExit()
    {
        GetComponent<Renderer>().material.color = Color.white;
    }
}
