using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightControl : MonoBehaviour
{
    public LightPatternList_SO lightData;

    private Light2D currentLight;

    private LightDetails currentLightDetails;

    private void Awake()
    {
        currentLight = GetComponent<Light2D>();
    }

    //ʵ���л��ƹ�
    public void ChangeLightShift(Season season, LightShift lightShift, float timeDifference)
    {
        //Debug.Log("����ChangeLightShift");
        currentLightDetails = lightData.GetLightDetails(season, lightShift);

        //������
        if (timeDifference < Settings.lightChangeDuration)
        {
            var colorOffset = (currentLightDetails.lightColor - currentLight.color) / Settings.lightChangeDuration * timeDifference;
            currentLight.color += colorOffset;
            DOTween.To(() => currentLight.color, c => currentLight.color = c, currentLightDetails.lightColor, Settings.lightChangeDuration - timeDifference);
            DOTween.To(() => currentLight.intensity, i => currentLight.intensity = i, currentLightDetails.lightIntensity, Settings.lightChangeDuration - timeDifference);
        }
        //ֱ�ӱ�
        if (timeDifference >= Settings.lightChangeDuration)
        {
            currentLight.color = currentLightDetails.lightColor;
            currentLight.intensity = currentLightDetails.lightIntensity;
        }
    }
}
