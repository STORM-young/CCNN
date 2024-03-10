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
        /// �洢�������꣬string��������
        /// </summary>
        public Dictionary<string, SerializableVector3> characterPosDict;

        //��¼����item
        public Dictionary<string, List<SceneItem>> sceneItemDict;
        //������������
        public Dictionary<string, List<SceneFurniture>> sceneFunitureDict;

        //����������+���꣩�Ͷ�Ӧ����Ƭ��Ϣ
        public Dictionary<string, TileDetails> tileDetailsDict;

        //�����Ƿ��һ�μ���
        public Dictionary<string, bool> firstLoadDict;
        //��������������
        public Dictionary<string, List<InventoryItem>> inventoryDict;

        public Dictionary<string, int> timeDict;

        public int playerMoney;

        //NPC
        public string targetScene;

        public int animationInstanceID;

        public bool interactable;

    }
}

