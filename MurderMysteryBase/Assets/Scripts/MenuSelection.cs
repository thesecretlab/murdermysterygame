using UnityEngine;
using System.Collections;

public class MenuSelection : MonoBehaviour
{
    public bool newGame;
    public bool options;
    public bool quit;
    // Use this for initialization

    void OnMouseUp()
    {
        //Currently both options select the map screen, easy to change.
        if (newGame)
        {
            Application.LoadLevel(1);
        }
       
        if (options)
        {
            Application.LoadLevel(1);
        }
        if (quit)
        {
            Application.Quit();
        }
    }
}