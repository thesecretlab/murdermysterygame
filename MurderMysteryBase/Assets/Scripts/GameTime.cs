using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Yarn.Unity;
using Yarn.Unity.GameScripts;

//!  GameTime Control Class
/*!
 This class controls the in game clock, tracks the current time, and includes Yarn Spinner accesible functions for incrementing the time.
*/

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

    public bool countTime = true;

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
        totalGameSeconds = (20 * 60 * 60);
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

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (countTime)
        {
            totalGameSeconds += secondsPerSecond * Time.deltaTime;
        }
        

		seconds = totalGameSeconds;
		minutes = totalGameSeconds / 60;
		hours = minutes / 60;
		days = hours / 24;

		second = totalGameSeconds % 60;
		minute = (totalGameSeconds / 60) % 60;
		hour = (minutes / 60) % 24;

	}

    /*! This function accepts upo to three floats, seconds, minutes, and hours, andsets the current in-game time accordingly.*/

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

    /*! This function accepts up to three floats, seconds, minutes, and hours, and increments the current in-game time accordingly.*/

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

    /*! This function returns the current in-game time as a formatted string.
     *  Without any parameters the function returns a 24 hours time format. If true is passed as a parameter, the time is returned as a 12 hour time format with am/pm appended.*/

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

    /*! This Yarn Spinner Accessible function accepts a boolean variable and freezes and unfreezes the in-game clock based on that variable. 
     *  Passing 'true' freezes the clock and passing 'false' unfreezes it.*/

    [YarnCommand("freezeTime")]
    public void freezeTime(string freeze)
    {
        Debug.Log("Freezing");
        if(freeze=="true")
        {
            countTime = false;
        }
        else
        {
            countTime = true;
        }
    }

    /*! This Yarn Spinner Accessible function accepts two strings, 'addTime' and 'timeUnit' and adds time to the in-game clock based on those variable.
     *  'addTime' is the number of time units to add and 'timeUnit' is the unit to add, either 'seconds', 'minutes', or hours.*/

    [YarnCommand("addTime")]
    public void addTime(string addTime, string timeUnit)
    {
        int parsedTime = 0;
        if (int.TryParse(addTime, out parsedTime))
        {
            switch (timeUnit)
            {
                case "seconds":
                    addGameTime(parsedTime);
                    break;
                case "minutes":
                    addGameTime(0,parsedTime);
                    break;
                case "hours":
                    addGameTime(0, 0, parsedTime);
                    break;
                default:
                    Debug.Log("Invalid Units");
                    break;
            }
        }
        else
        {
            Debug.Log("Invalid Time");
        }
        
    }
}
