using UnityEngine;
using System.Collections;

public class MapSelection : MonoBehaviour {

    public bool morgue;
    public bool office;
    public bool murderScene;

    void OnMouseUp()
    {
        //to do: scene selection and  maybe game timer?
        if (morgue)
        {
            Application.LoadLevel(1/*Morgue scene number goes here*/);
        }
        if (office)
        {
            Application.LoadLevel(1);
        }
        if (murderScene)
        {
            Application.LoadLevel(1);
        }
    }
}

