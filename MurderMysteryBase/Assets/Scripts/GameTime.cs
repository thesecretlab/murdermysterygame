using UnityEngine;
using System.Collections;

public class GameTime : MonoBehaviour {

	public float totalGameSeconds;

	public float seconds;
	public float minutes;
	public float hours;
	public float days;

	public float second;
	public float minute;
	public float hour;

	private float secondsPerSecond;

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

		second = totalGameSeconds % 60;
		minute = (totalGameSeconds / 60) % 60;
		hour = (minutes / 60) % 24;

	}

	public void setGameTime(float seconds)
	{
		totalGameSeconds = seconds;
	}

	public void setGameTime(float seconds, float minutes)
	{
		totalGameSeconds = seconds + (minutes*60);
	}

	public void setGameTime(float seconds, float minutes, float hours)
	{
		totalGameSeconds = seconds + (minutes * 60) + (hours*3600);
	}

	public void addGameTime(float seconds)
	{
		totalGameSeconds += seconds;
	}

	public void addGameTime(float seconds, float minutes)
	{
		totalGameSeconds += seconds + (minutes * 60);
	}

	public void addGameTime(float seconds, float minutes, float hours)
	{
		totalGameSeconds += seconds + (minutes * 60) + (hours * 3600);
	}


    public string getGameTimeString()
    {
        return string.Format("{0:00}:{1:00}:{2:00}", (int)hour, (int)minute, (int)second);
    }
}
