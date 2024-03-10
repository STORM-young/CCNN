using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    private LightControl[] sceneLights;

    private LightShift currentLightShift;

    private Season currentSeason;

    private float timeDifference=Settings.lightChangeDuration;

    private void OnEnable()
    {
        EventHandler.AfterSceneLoadedEvent += OnAfterSceneLoadedEvent;
        EventHandler.LightShiftChangeEvent += OnLightShiftChangeEvent;
        EventHandler.StartNewGameEvent += OnStartNewGameEvent;
    }

    private void OnDisable()
    {
        EventHandler.AfterSceneLoadedEvent -= OnAfterSceneLoadedEvent;
        EventHandler.LightShiftChangeEvent -= OnLightShiftChangeEvent; 
        EventHandler.StartNewGameEvent -= OnStartNewGameEvent;
    }

    private void OnAfterSceneLoadedEvent()
    {
        sceneLights = FindObjectsOfType<LightControl>();

        foreach(LightControl light in sceneLights)
        {
            //LightControl �ı�ƹ�ķ���
            light.ChangeLightShift(currentSeason, currentLightShift, timeDifference);
        }
    }

    private void OnStartNewGameEvent(int obj)
    {
        currentLightShift = LightShift.Morning;
    }

    private void OnLightShiftChangeEvent(Season season, LightShift lightShift, float timeDifference)
    {
        this.timeDifference = timeDifference;
        currentSeason = season;
        //Debug.Log("OnLightShiftChangeEvent");

        if (currentLightShift != lightShift)
        {
            //Debug.Log("currentLightShift=" + currentLightShift + ",lightShift=" + lightShift);
            currentLightShift = lightShift;
            foreach (LightControl light in sceneLights)
            {
                //LightControl �ı�ƹ�ķ���
                light.ChangeLightShift(currentSeason, currentLightShift, timeDifference);
            }
        }  
    }
}