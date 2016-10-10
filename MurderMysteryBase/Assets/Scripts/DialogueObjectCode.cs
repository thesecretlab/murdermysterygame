using UnityEngine;
using System.Collections;

//!  Dialogue Object Persistance Code
/*!
 This class keeps the attached Dialogue Object alive through scene transitions and ensures no duplicate Dialogue Objects persist in the scene.
*/

public class DialogueObjectCode : MonoBehaviour {

    private static DialogueObjectCode _instance;

    void Awake()
    {
        //if we don't have an [_instance] set yet
        if (!_instance)
            _instance = this;
        //otherwise, if we do, kill this thing
        else
            Destroy(this.gameObject);


        DontDestroyOnLoad(this.gameObject);
    }

}
