using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MFarm.Transition; 

namespace MFarm.Save
{
    public class DataSlot
    {
        /// <summary>
        /// 存档条,string是GUID
        /// </summary>
        public Dictionary<string, GameSaveData> dataDict = new Dictionary<string, GameSaveData>();

        #region UI显示存档详情
        public string DataTime
        {
            get
            {
                var key = TimeManager.Instance.GUID;

                if (dataDict.ContainsKey(key))
                {
                    var timeData = dataDict[key];
                    return timeData.timeDict["gameYear"] + "年/" + (Season)timeData.timeDict["gameSeason"] + "/" + timeData.timeDict["gameMonth"] + "月/" + timeData.timeDict["gameDay"] + "日/";
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
                        "00Start" => "海边",
                        "01Field" => "农场",
                        "02_Home" => "家",
                        "03Town" => "城镇",
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


