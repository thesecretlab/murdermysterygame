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
        switch(name){
            case "New Game":
                GetComponent<MenuSelection>().newGame = true;
                break;
            case "Quit":
                GetComponent<MenuSelection>().quit = true;
                break;
            case "skip":
                GetComponent<MenuSelection>().options = true;
                break;
        }
        
    }

    void OnMouseExit()
    {
        GetComponent<Renderer>().material.color = Color.white;
        switch (name)
        {
            case "New Game":
                GetComponent<MenuSelection>().newGame = false;
                break;
            case "Quit":
                GetComponent<MenuSelection>().quit = false;
                break;
            case "skip":
                GetComponent<MenuSelection>().options = false;
                break;
        }
    }
}
