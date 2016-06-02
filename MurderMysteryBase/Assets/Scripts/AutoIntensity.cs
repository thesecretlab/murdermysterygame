using UnityEngine;
using System.Collections;

public class AutoIntensity : MonoBehaviour {

	public Gradient nightDayColor;

	public float maxAmbient = 1f;
	public float minAmbient = 0f;
	public float minAmbientPoint = -0.2f;

	public Gradient nightDayFogColor;
	public AnimationCurve fogDensityCurve;
	public float fogScale = 1f;

	public float dayAtmosphereThickness = 0.4f;
	public float nightAtmosphereThickness = 0.87f;

    public float angle;

    float skySpeed = 1;

	GameTime time;

	//Light mainLight;
	Skybox sky;
	Material skyMat;

	void Start () 
	{
	
		//mainLight = GetComponent<Light>();
		skyMat = RenderSettings.skybox;

	}

	void Update () 
	{
        time = GameObject.Find("HUD").GetComponent<GameTime>();

        angle = (time.hour * 15);

        if(angle>180f)
        {
            angle = 180f - (angle - 180f);
        }
        angle = (angle / 180f);
		float i = ((maxAmbient - minAmbient) * angle) + minAmbient;

        RenderSettings.ambientIntensity = i;

		RenderSettings.ambientLight = nightDayColor.Evaluate(angle);

		RenderSettings.fogColor = nightDayFogColor.Evaluate(angle);
		RenderSettings.fogDensity = fogDensityCurve.Evaluate(angle) * fogScale;

		i = ((dayAtmosphereThickness - nightAtmosphereThickness) * angle) + nightAtmosphereThickness;
		skyMat.SetFloat ("_AtmosphereThickness", i);

	}
}
