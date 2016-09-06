using UnityEngine;
using System.Collections;

public class LightFlash : MonoBehaviour {

    private float delta;
    private Light thisLight;
    private bool isOn = false;
    private AudioSource ringring;

    public bool isRinging = true;


	void Start () {
        thisLight = this.GetComponent<Light>();
        ringring = this.GetComponent<AudioSource>();
    }


    void Update() {
        if (isRinging)
        {
            if (isOn)
            {
                thisLight.intensity = 1.0f;
            }
            else
            {
                thisLight.intensity = 0.0f;
            }

            delta += Time.deltaTime;
            if (delta > 0.5f)
            {
                delta = 0.0f;
                isOn = !isOn;
            }
            if (!ringring.isPlaying)
            {
                ringring.Play();
            }
        }
        else
        {
            thisLight.intensity = 0.0f;
            if (ringring.isPlaying)
            {
                ringring.Stop();
            }
        }

    }
}
