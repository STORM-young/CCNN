using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class ItemDetails
{
    public int itemID;

    public string itemName;

    public ItemType itemType;
    //��Ʒͼ��
    public Sprite itemIcon;

    //��ͼ����ʾʱ��ͼƬ
    public Sprite itemOnWorldSprite;

    //��Ʒ������
    public string itemDescription;

    //��ʹ�õķ�Χ
    public int itemUseRadius;

    public bool canPickedup;

    public bool canDropped;

    public bool canCarried;

    public int itemPrice;

    [Range(0, 1)]
    public float sellPercentage;
}
//�˴�ʹ��struct����ʹ��class��ԭ����
//1������class����ô�ж�ʱ����Ҫ�ж����Ƿ�Ϊ�գ�����յ��߼��ڱ�������ʱ�������һЩ�鷳��
//�������struct����ʼ��ʱ���itemID��ΪĬ��ֵ0����ô��ֻ���ж����ID�ǲ���0����С�ڶ��پ����ж��ǲ��ǿ���
//2�������UI�������ʾ��ǰ�ұ�������Ʒ�������amount����0���ǾͿ����ñ����������ֱ����0������Ҫ����ɾ����Ϊɾ�����ܵ��±��������ϳ�������
[System.Serializable]
public struct InventoryItem
{
    public int itemID;

    public int itemAmount;
}

[System.Serializable]
public class AnimatorType
{
    public PartType partType;

    public PartName partName;

    public AnimatorOverrideController overrideController;

}

//Ҫ���������
[System.Serializable]
public class SerializableVector3
{
    public float x, y, z;

    public SerializableVector3(Vector3 pos)
    {
        this.x = pos.x;
        this.y = pos.y;
        this.z = pos.z;
    }

    public Vector3 ToVector3()
    {
        return new Vector3(x, y, z);
    }

    public Vector2Int ToVector2Int()
    {
        return new Vector2Int((int)x, (int)y);
    }
}

//�����е���Ʒ
[System.Serializable]
public class SceneItem
{
    public int itemID;

    public SerializableVector3 position;
}

[System.Serializable]
public class SceneFurniture
{
    public int itemID;

    public SerializableVector3 position;

    public int boxIndex;
}

[System.Serializable]
public class TileProperty
{
    //��Ƭ����
    public Vector2Int tileCoordinate;
    //��Ƭ����
    public GridType gridType; 

    public bool boolTypeValue;
}

[System.Serializable]
public class TileDetails
{
    public int gridX, gridY;

    public bool canDig;

    public bool canDropItem;

    public bool canPlaceFurniture;

    public bool isNPCObstacle;
    //�ڿӹ�ȥ������
    public int daysSinceDug = -1;
    //��ˮ��ȥ������
    public int daysSinceWatered = -1;
    //�ֵ�����ID
    public int seedItemID = -1;
    //�ɳ����� 
    public int growthDays = -1;
    //�ϴ��ջ�����
    public int daysSinceLastHarvest = -1; 
}  

[System.Serializable]
public class NPCPosition
{
    public Transform npc;

    public string startScene;

    public Vector3 position;
}

//����·��
[System.Serializable]
public class ScenePath
{
    public string sceneName;

    public Vector2Int fromGridCell;

    public Vector2Int gotoGridCell;
}

[System.Serializable]
public class SceneRoute
{
    public string fromSceneName;

    public string gotoSceneName;

    public List<ScenePath> scenePathList;
}

