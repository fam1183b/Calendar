using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Calendar : MonoBehaviour
{
    public Transform[] weeks;
    public Text MonthAndYear;
    private List<Day> days = new List<Day>();
    private DateTime currDate = DateTime.Now;
    
    private void Start()
    {
        UpdateCalendar(DateTime.Now.Year, DateTime.Now.Month);
    }
    
    void UpdateCalendar(int year, int month)
    {
        DateTime temp = new DateTime(year, month, 1);
        currDate = temp;
        MonthAndYear.text = temp.ToString("MMMM yyyy");

        //Воскресенье == 0, Суббота == 6
        int startDay = StartDay(temp);
        int endDay = DateTime.DaysInMonth(year, month);
        
        if(days.Count == 0)
        {
            for (int w = 0; w < 6; w++)
            {
                for (int i = 0; i < 7; i++)
                {
                    Day newDay;
                    int currDay = (w * 7) + i;
                    if (currDay < startDay || currDay - startDay >= endDay)
                    {
                        newDay = new Day(currDay - startDay, Color.grey,weeks[w].GetChild(i).gameObject);
                    }
                    else
                    {
                        newDay = new Day(currDay - startDay, Color.white,weeks[w].GetChild(i).gameObject);
                    }
                    days.Add(newDay);
                }
            }
        }
        else
        {
            for(int i = 0; i < 42; i++)
            {
                if(i < startDay || i - startDay >= endDay)
                {
                    days[i].UpdateColor(Color.grey);
                }
                else
                {
                    days[i].UpdateColor(Color.white);
                }

                days[i].UpdateDay(i - startDay);
            }
        }
        
        if(DateTime.Now.Year == year && DateTime.Now.Month == month)
        {
            days[(DateTime.Now.Day - 1) + startDay].UpdateColor(Color.green);
        }
    }
    public int StartDay(DateTime temp)
    {
        int i = 0;
        switch (((int)temp.DayOfWeek).ToString())
        {
            case "0":
                i = 6;
                break;
            case "1":
                i = 0;
                break;
            case "2":
                i = 1;
                break;
            case "3":
                i = 2;
                break;
            case "4":
                i = 3;
                break;
            case "5":
                i = 4;
                break;
            case "6":
                i = 5;
                break;
        }
        return i;
    }

    public void SwitchMonth(int direction)
    {
        if(direction < 0)
        {
            currDate = currDate.AddMonths(direction);
        }
        else
        {
            currDate = currDate.AddMonths(direction);
        }
        if (direction == 0)
        {
            currDate = DateTime.Now;
        }

        UpdateCalendar(currDate.Year, currDate.Month);
    }

    public class Day
    {
        public int dayNum;
        public Color dayColor;
        public GameObject obj;
        
        public Day(int dayNum, Color dayColor, GameObject obj)
        {
            this.dayNum = dayNum;
            this.obj = obj;
            UpdateColor(dayColor);
            UpdateDay(dayNum);
        }
        
        public void UpdateColor(Color newColor)
        {
            obj.GetComponent<Image>().color = newColor;
            dayColor = newColor;
        }
        
        public void UpdateDay(int newDayNum)
        {
            dayNum = newDayNum;
            if (dayColor == Color.white || dayColor == Color.green)
            {
                obj.GetComponentInChildren<Text>().text = (dayNum + 1).ToString();
            }
            else
            {
                obj.GetComponentInChildren<Text>().text = "";
            }
        }
    }
}
