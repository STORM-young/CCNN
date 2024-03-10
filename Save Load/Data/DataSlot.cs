using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MFarm.Transition; 

namespace MFarm.Save
{
    public class DataSlot
    {
        /// <summary>
        /// �浵��,string��GUID
        /// </summary>
        public Dictionary<string, GameSaveData> dataDict = new Dictionary<string, GameSaveData>();

        #region UI��ʾ�浵����
        public string DataTime
        {
            get
            {
                var key = TimeManager.Instance.GUID;

                if (dataDict.ContainsKey(key))
                {
                    var timeData = dataDict[key];
                    return timeData.timeDict["gameYear"] + "��/" + (Season)timeData.timeDict["gameSeason"] + "/" + timeData.timeDict["gameMonth"] + "��/" + timeData.timeDict["gameDay"] + "��/";
                }
                else return string.Empty;
            }
        }

        public string DataScene
        {
            get
            {
                var key = TransitionManager.Instance.GUID;
                if (dataDict.ContainsKey(key))
                {
                    var transitionData = dataDict[key];
                    return transitionData.dataSceneName switch
                    {
                        "00Start" => "����",
                        "01Field" => "ũ��",
                        "02_Home" => "��",
                        "03Town" => "����",
                        _ => string.Empty
                    };
                }
                else
                {
                    return string.Empty;
                }
            }
        }
        #endregion
    }
}


