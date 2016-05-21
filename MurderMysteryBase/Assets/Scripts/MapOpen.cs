using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;
using System.Collections;

public class MapOpen : MonoBehaviour
{
    public Camera FPSCamera;
    public Camera MapCamera;

    public FirstPersonController firstPersonController;

    private static MapOpen _instance;

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

    void Start()
    {
        firstPersonController = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>(); //declare local pointer to the players first person controller object
        FPSCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        MapCamera = GameObject.FindGameObjectWithTag("MapCamera").GetComponent<Camera>();
        FPSCamera.enabled = true;
        MapCamera.enabled = false;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            cameraSwitch();
        }
    }

    public void cameraSwitch()
    {
        FPSCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        MapCamera = GameObject.FindGameObjectWithTag("MapCamera").GetComponent<Camera>();
        FPSCamera.enabled = !FPSCamera.enabled;
        MapCamera.enabled = !MapCamera.enabled;
        firstPersonController.LockControllerReleaseMouse(MapCamera.enabled);
    }

    void OnLevelWasLoaded(int level)
    {
        firstPersonController = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>(); //declare local pointer to the players first person controller object
        FPSCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        MapCamera = GameObject.FindGameObjectWithTag("MapCamera").GetComponent<Camera>();
    }
}

