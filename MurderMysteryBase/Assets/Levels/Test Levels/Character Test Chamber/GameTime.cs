using UnityEngine;
using System.Collections;

public class GameTime : MonoBehaviour {

	public double totalGameSeconds;

	public double seconds;
	public double minutes;
	public double hours;
	public double days;
	public double months;
	public double years;

	public double second;
	public double minute;
	public double hour;

	private double secondsPerSecond;

	void Start()
	{
		secondsPerSecond = 1;
		totalGameSeconds += secondsPerSecond * Time.deltaTime;
	}


	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			secondsPerSecond = 1;
		}
		else if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			secondsPerSecond = 60;
		}
		else if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			secondsPerSecond = 3600;
		}
		else if (Input.GetKeyDown(KeyCode.Alpha4))
		{
			secondsPerSecond = 20000;
		}
		else if (Input.GetKeyDown(KeyCode.Alpha5))
		{
			secondsPerSecond = 86400;
		}

		totalGameSeconds += secondsPerSecond * Time.deltaTime;

		seconds = totalGameSeconds;
		minutes = totalGameSeconds / 60;
		hours = minutes / 60;
		days = hours / 24;
		months = days / (365 / 12);
		years = months / 12;

		second = totalGameSeconds % 60;
		minute = (totalGameSeconds / 60) % 60;
		hour = (minutes / 60) % 24;

	}


	void OnGUI()
	{

		//GUI.Label(new Rect(0, 200, 500, 500), "Total Seconds: " + totalGameSeconds);
		//GUI.Label(new Rect(0, 225, 500, 500), "Seconds Per Second: " + secondsPerSecond);
		//GUI.Label(new Rect(0, 250, 500, 500), "Second: " + (int)seconds % 60);
		//GUI.Label(new Rect(0, 275, 500, 500), "Minute: " + (int)minutes % 60);
		//GUI.Label(new Rect(0, 300, 500, 500), "Hour: " + (int)hours % 24);
		//GUI.Label(new Rect(0, 325, 500, 500), "Day: " + (int)days % (365 / 12));
		//GUI.Label(new Rect(0, 350, 500, 500), "Month: " + (int)months % 12);
		//GUI.Label(new Rect(0, 375, 500, 500), "Year: " + (int)years);

		
		//GUI.Label(new Rect(0, 375, 500, 500), niceTime);

	}

    public string getGameTimeString()
    {
        return string.Format("{0:00}:{1:00}:{2:00}", (int)hour, (int)minute, (int)second);
    }
}
