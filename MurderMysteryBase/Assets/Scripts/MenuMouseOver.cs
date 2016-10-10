using UnityEngine;
using System.Collections;

//!  Menu Mouse Over Class
/*!
 Controls the apperance of main menu options based on mouse-over.
*/

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
