using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

public class CalendarManager : MonoBehaviour
{
    public DateTime CurrentDate = new DateTime(2022, 4, 11);
    public TimeSlot CurrentTime;
    public Weather CurrentWeather;

    public TextMeshProUGUI DateText;
    public TextMeshProUGUI DayText;
    public TextMeshProUGUI TimeSlot;

    public Image WeatherIcon;

    public Sprite[] WeatherSprites;
    public string[] TimeString;
    



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DateText.text = CurrentDate.ToString("MM/dd");
        DayText.text =  CurrentDate.ToString("ddd");
        TimeSlot.text = TimeString[(int)CurrentTime];

        WeatherIcon.sprite = WeatherSprites[(int)CurrentWeather];
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