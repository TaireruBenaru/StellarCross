using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CalendarManager : MonoBehaviour
{
    public DateTime CurrentDate = new DateTime(2022, 4, 11);
    public TimeSlot CurrentTime;
    public Weather CurrentWeather;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public enum TimeSlot
{
    Daybreak,
    Morning,
    Afternoon,
    Daytime,
    AfterSchool,
    Evening,
    Night
}

public enum Weather
{
    Clear,
    Cloudy,
    Rainy,
    Snowy
}