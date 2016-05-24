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

    public bool TimeKeyEnabled = false;

    private float secondsPerSecond;

	private static GameTime _instance;

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
		secondsPerSecond = 1;
        totalGameSeconds = (8 * 60 * 60);
		totalGameSeconds += secondsPerSecond * Time.deltaTime;
	}


	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Alpha1) && TimeKeyEnabled)
		{
			secondsPerSecond = 1;
		}
		else if (Input.GetKeyDown(KeyCode.Alpha2) && TimeKeyEnabled)
		{
			secondsPerSecond = 60;
		}
		else if (Input.GetKeyDown(KeyCode.Alpha3) && TimeKeyEnabled)
		{
			secondsPerSecond = 3600;
		}
		else if (Input.GetKeyDown(KeyCode.Alpha4) && TimeKeyEnabled)
		{
			secondsPerSecond = 20000;
		}
		else if (Input.GetKeyDown(KeyCode.Alpha5) && TimeKeyEnabled)
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
		return getGameTimeString(false);
	}

    public string getGameTimeString(bool ampm)
    {
		if(ampm)
		{
			if (hour < 12)
			{
				return string.Format("{0:00}:{1:00}am", (int)hour, (int)minute);
			}
			else
			{
				return string.Format("{0:00}:{1:00}pm", (int)hour-12, (int)minute);
			}

		}
		else
		{
			return string.Format("{0:00}:{1:00}:{2:00}", (int)hour, (int)minute, (int)second);
		}
        
    }
}
