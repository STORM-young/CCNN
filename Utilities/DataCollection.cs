using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class ItemDetails
{
    public int itemID;

    public string itemName;

    public ItemType itemType;
    //物品图标
    public Sprite itemIcon;

    //地图上显示时的图片
    public Sprite itemOnWorldSprite;

    //物品的描述
    public string itemDescription;

    //可使用的范围
    public int itemUseRadius;

    public bool canPickedup;

    public bool canDropped;

    public bool canCarried;

    public int itemPrice;

    [Range(0, 1)]
    public float sellPercentage;
}
//此处使用struct而不使用class的原因有
//1、创建class，那么判断时就需要判断它是否为空，这个空的逻辑在背包中有时候会引起一些麻烦。
//而如果用struct，初始化时会把itemID设为默认值0，那么我只需判断这个ID是不是0或者小于多少就能判断是不是空了
//2、而针对UI方面会显示当前我背包中物品数，如果amount等于0，那就可以让背包这个东西直接清0，不需要将它删除因为删除可能导致背包排序上出现问题
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

//要保存的坐标
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

//场景中的物品
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
    //瓦片坐标
    public Vector2Int tileCoordinate;
    //瓦片类型
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
    //挖坑过去的天数
    public int daysSinceDug = -1;
    //浇水过去的天数
    public int daysSinceWatered = -1;
    //种的种子ID
    public int seedItemID = -1;
    //成长天数 
    public int growthDays = -1;
    //上次收获天数
    public int daysSinceLastHarvest = -1; 
}  

[System.Serializable]
public class NPCPosition
{
    public Transform npc;

    public string startScene;

    public Vector3 position;
}

//场景路径
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

