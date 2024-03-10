using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MFarm.Save
{
    [System.Serializable]
    public class GameSaveData
    {
        public string dataSceneName;
        /// <summary>
        /// 存储人物坐标，string人物名字
        /// </summary>
        public Dictionary<string, SerializableVector3> characterPosDict;

        //记录场景item
        public Dictionary<string, List<SceneItem>> sceneItemDict;
        //场景建造数据
        public Dictionary<string, List<SceneFurniture>> sceneFunitureDict;

        //（场景名字+坐标）和对应的瓦片信息
        public Dictionary<string, TileDetails> tileDetailsDict;

        //场景是否第一次加载
        public Dictionary<string, bool> firstLoadDict;
        //背包和箱子数据
        public Dictionary<string, List<InventoryItem>> inventoryDict;

        public Dictionary<string, int> timeDict;

        public int playerMoney;

        //NPC
        public string targetScene;

        public int animationInstanceID;

        public bool interactable;

    }
}

