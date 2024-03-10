using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ScheduleDetails:IComparable<ScheduleDetails>
{
    public int hour, minute, day;

    public int priority;    //优先级越小越先执行

    public Season season;

    public string targetScene;

    public Vector2Int targetGridPosition;

    public AnimationClip clipAtStop;

    public bool interactable;

    public ScheduleDetails(int hour,int minute,int day,int priority,Season season,string targetScene,Vector2Int targetGridPosition, AnimationClip clipAtStop, bool interactable)
    {
        this.hour = hour;
        this.minute = minute;
        this.day = day;
        this.priority = priority;
        this.season = season;
        this.targetScene = targetScene;
        this.targetGridPosition = targetGridPosition;
        this.clipAtStop = clipAtStop;
        this.interactable = interactable;
    }

    public int Time => (hour * 100) + minute;

    public int CompareTo(ScheduleDetails other)
    {
        //比较选出最低的F值，返回1,-1,0
        if (Time == other.Time)
        {
            if (priority > other.priority)
                return 1;
            else
                return -1;
        }
        else if (Time > other.Time)
        {
            return 1;
        }
        else if (Time < other.Time)
        {
            return -1;
        }
        return 0;
    }
}
