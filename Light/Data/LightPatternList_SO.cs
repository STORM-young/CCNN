using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LightPatternList_SO", menuName = "Light/Light Pattern")]
public class LightPatternList_SO : ScriptableObject
{
    public List<LightDetails> lightPatternList;

    /// <summary>
    /// ���ݼ��ں������صƹ�����
    /// </summary>
    /// <param name="season"></param>
    /// <param name="lightShift"></param>
    /// <returns></returns>
    public LightDetails GetLightDetails(Season season, LightShift lightShift)
    {
        return lightPatternList.Find(l => l.season == season && l.lightShift == lightShift);
    }
}

[System.Serializable]
public class LightDetails
{
    public Season season;

    public Color lightColor;

    public LightShift lightShift;

    public float lightIntensity;
}
