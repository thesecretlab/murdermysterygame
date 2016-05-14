using UnityEngine;
using System.Collections;

public class AutoIntensity : MonoBehaviour {

	public Gradient nightDayColor;

	public float maxIntensity = 3f;
	public float minIntensity = 0f;
	public float minPoint = -0.2f;

	public float maxAmbient = 1f;
	public float minAmbient = 0f;
	public float minAmbientPoint = -0.2f;


	public Gradient nightDayFogColor;
	public AnimationCurve fogDensityCurve;
	public float fogScale = 1f;

	public float dayAtmosphereThickness = 0.4f;
	public float nightAtmosphereThickness = 0.87f;

	public Vector3 dayRotateSpeed;
	public Vector3 nightRotateSpeed;

	float skySpeed = 1;

	GameTime time;

	Light mainLight;
	Skybox sky;
	Material skyMat;

	void Start () 
	{
	
		mainLight = GetComponent<Light>();
		skyMat = RenderSettings.skybox;

	}

	void Update () 
	{
	
		float tRange = 1 - minPoint;
		float dot = Mathf.Clamp01 ((Vector3.Dot (mainLight.transform.forward, Vector3.down) - minPoint) / tRange);
		float i = ((maxIntensity - minIntensity) * dot) + minIntensity;

		time = GameObject.Find("GameTimeObject").GetComponent<GameTime>();

		mainLight.intensity = i;

		tRange = 1 - minAmbientPoint;
		dot = Mathf.Clamp01 ((Vector3.Dot (mainLight.transform.forward, Vector3.down) - minAmbientPoint) / tRange);
		i = ((maxAmbient - minAmbient) * dot) + minAmbient;
		RenderSettings.ambientIntensity = i;

		mainLight.color = nightDayColor.Evaluate(dot);
		RenderSettings.ambientLight = mainLight.color;

		RenderSettings.fogColor = nightDayFogColor.Evaluate(dot);
		RenderSettings.fogDensity = fogDensityCurve.Evaluate(dot) * fogScale;

		i = ((dayAtmosphereThickness - nightAtmosphereThickness) * dot) + nightAtmosphereThickness;
		skyMat.SetFloat ("_AtmosphereThickness", i);

		float angle = (time.hour * 15)-90;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.right);

		/*if (dot > 0) 
		{
			//transform.Rotate (dayRotateSpeed * Time.deltaTime * skySpeed);

			//Debug.Log(transform.rotation);
			//Debug.Log(transform.localRotation);
			
		}
		else
		{
			//transform.Rotate (nightRotateSpeed * Time.deltaTime * skySpeed);
			//Debug.Log(transform.rotation);
			//Debug.Log(transform.localRotation);
		}*/


		if (Input.GetKeyDown(KeyCode.Q)) time.addGameTime(0, 0, -1);
		if (Input.GetKeyDown(KeyCode.F)) transform.rotation *= Quaternion.AngleAxis(10, Vector3.right);
		if (Input.GetKeyDown(KeyCode.E)) time.addGameTime(0, 0, 1);


	}
}
