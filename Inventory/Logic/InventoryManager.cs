using System.Collections;
using System.Collections.Generic;
using MFarm.Save;
using UnityEngine;

namespace MFarm.Inventory
{
    public class InventoryManager : Singleton<InventoryManager>,ISaveable
    {
        [Header("物品数据")]
        public ItemDataList_SO itemDataList_SO;

        [Header("建造蓝图")]
        public BluePrintDataList_SO bluePrintData;

        [Header("背包数据")]
        public InventoryBag_SO playerBagTemp;

        public InventoryBag_SO playerBag;

        private InventoryBag_SO currentBoxBag;

        [Header("交易")]
        public int playerMoney;

        private Dictionary<string,List<InventoryItem>> boxDataDict = new Dictionary<string,List<InventoryItem>>();

        public int BoxDataAmount => boxDataDict.Count;

        public string GUID => GetComponent<DataGUID>().guid;

        private void OnEnable()
        {
            EventHandler.DropItemEvent += OnDropItemEvent;
            EventHandler.HarvestAtPlayerPosition += OnHarvestAtPlayerPosition;
            EventHandler.BuildFurnitureEvent += OnBuildFurnitureEvent;
            EventHandler.BaseBagOpenEvent += OnBaseBagOpenEvent;
            EventHandler.StartNewGameEvent += OnStartNewGameEvent;
        }

        private void OnDisable()
        {
            EventHandler.DropItemEvent -= OnDropItemEvent;
            EventHandler.HarvestAtPlayerPosition -= OnHarvestAtPlayerPosition;
            //建造
            EventHandler.BuildFurnitureEvent -= OnBuildFurnitureEvent;
            EventHandler.BaseBagOpenEvent -= OnBaseBagOpenEvent;
            EventHandler.StartNewGameEvent -= OnStartNewGameEvent;
        }

        private void Start()
        {
            ISaveable saveable = this;
            saveable.RegisterSaveable();
            //EventHandler.CallUpdateInventoryUI(InventoryLocation.Player, playerBag.itemList);
        }

        private void OnStartNewGameEvent(int obj)
        {
            playerBag = Instantiate(playerBagTemp);
            playerMoney = Settings.playerStartMoney;
            boxDataDict.Clear();
            EventHandler.CallUpdateInventoryUI(InventoryLocation.Player, playerBag.itemList);
        }

        private void OnBaseBagOpenEvent(SlotType slotType, InventoryBag_SO bag_SO)
        {
            //Debug.Log("OnBaseBagOpenEvent");
            currentBoxBag = bag_SO;
        }

        private void OnBuildFurnitureEvent(int ID, Vector3 pos)
        {
            RemoveItem(ID, 1);
            BluePrintDetails bluePrint = bluePrintData.GetBluePrintDetails(ID);
            foreach(var item in bluePrint.resourceItem)
            {
                RemoveItem(item.itemID, item.itemAmount);
            }
        }

            private void OnHarvestAtPlayerPosition(int ID)
        {
            //是否已经有该物品
            var index = GetItemIndexInBag(ID);

            AddItemAtIndex(ID, index, 1);

            EventHandler.CallUpdateInventoryUI(InventoryLocation.Player, playerBag.itemList);
        }

        private void OnDropItemEvent(int ID, Vector3 pos, ItemType itemType)
        {
            RemoveItem(ID, 1);
        }
        /// <summary> 
        /// 移除指定数量背包物品
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="removeAmount"></param>
        private void RemoveItem(int ID,int removeAmount)
        {
            var index = GetItemIndexInBag(ID);

            if (playerBag.itemList[index].itemAmount > removeAmount)
            {
                var amount = playerBag.itemList[index].itemAmount - removeAmount;
                var item = new InventoryItem { itemID = ID, itemAmount = amount };
                playerBag.itemList[index] = item;
            }
            else if(playerBag.itemList[index].itemAmount == removeAmount)
            {
                var item = new InventoryItem();
                playerBag.itemList[index] = item;
            }
            EventHandler.CallUpdateInventoryUI(InventoryLocation.Player, playerBag.itemList);
        }



        ///<summary>
        ///通过ID返回物品信息
        ///</summary>
        ///<param name="ID">Item ID</param>
        ///<returns></returns>
        public ItemDetails GetItemDetails(int ID)
        {
            return itemDataList_SO.itemDetailsList.Find(i => i.itemID == ID);
        }

        ///<summary>
        ///添加物品到Player背包
        ///</summary>
        ///<param name="item"></param>
        ///<param name="toDestroy">是否要销毁物品</param>
        public void AddItem(Item item,bool toDestroy)
        {
            //是否已经有该物品
            var index = GetItemIndexInBag(item.itemID);

            AddItemAtIndex(item.itemID, index, 1);

            //Debug.Log(GetItemDetails(item.itemID).itemID + "Name: " + GetItemDetails(item.itemID).itemName);
             if(toDestroy)
             {
                Destroy(item.gameObject);
             }
            //更新UI
            EventHandler.CallUpdateInventoryUI(InventoryLocation.Player, playerBag.itemList);
        }

        ///<summary>
        ///检查背包是否有空位
        ///</summary>
        ///<returns></returns>
        private bool CheckBagCapacity()
        {
            for(int i=0;i<playerBag.itemList.Count; i++)
            {
                if (playerBag.itemList[i].itemID == 0)
                    return true;
            }
            return false;
        }
        ///<summary>
        ///添加物品到Player背包
        ///</summary>
        ///<param name="ID">物品ID</param>
        ///<returns>没有则返回-1，否则返回这物品在背包中的序号</returns>
        private int GetItemIndexInBag(int ID)
        {
            for (int i = 0; i < playerBag.itemList.Count; i++)
            {
                if (playerBag.itemList[i].itemID == ID)
                    return i;
            }
            return -1;
        }

        ///<summary>
        ///在指定背包位置添加物品
        ///</summary>
        ///<param name="ID">物品ID</param>
        ///<param name="index">背包中序号</param>
        ///<param name="amount">物品数量</param>
        ///<returns>没有则返回-1，否则返回这物品在背包中的序号</returns>
        private void AddItemAtIndex(int ID,int index,int amount)
        {
            if (index == -1&& CheckBagCapacity())    //背包没有这个物品  同时背包有空位
            {
                var item = new InventoryItem { itemID = ID, itemAmount = amount };
                for (int i = 0; i < playerBag.itemList.Count; i++)
                {
                    if (playerBag.itemList[i].itemID == 0)
                    { 
                        playerBag.itemList[i] = item;
                        break;
                    }
                }
            }
            else    //背包有这个物品
            {
                int currentAmount = playerBag.itemList[index].itemAmount + amount;
                var item = new InventoryItem { itemID = ID, itemAmount = currentAmount };
                playerBag.itemList[index] = item;
            }

        }
        /// <summary>
        /// Player背包范围内交换物品
        /// </summary>
        /// <param name="fromIndex">起始序号</param>
        /// <param name="targetIndex">目标序号</param>
        public void SwapItem(int fromIndex,int targetIndex)
        {
            InventoryItem currentItem = playerBag.itemList[fromIndex];
            InventoryItem targetItem = playerBag.itemList[targetIndex];

            if(targetItem.itemID != 0)
            {
                playerBag.itemList[fromIndex] = targetItem;
                playerBag.itemList[targetIndex] = currentItem;
            }
            else
            {
                playerBag.itemList[targetIndex] = currentItem;
                playerBag.itemList[fromIndex] = new InventoryItem();
            }

            EventHandler.CallUpdateInventoryUI(InventoryLocation.Player, playerBag.itemList);
        }

        /// <summary>
        ///根据位置返回背包数据 
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        private List<InventoryItem> GetItemList(InventoryLocation location)
        {
            return location switch
            {
                InventoryLocation.Player => playerBag.itemList,
                InventoryLocation.Box => currentBoxBag.itemList,
                _ => null
            };
        }

        /// <summary>
        /// 跨背包交换数据
        /// </summary>
        /// <param name="locationFrom"></param>
        /// <param name="fromIndex"></param>
        /// <param name="locationTarget"></param>
        /// <param name="targetIndex"></param>
        public void SwapItem(InventoryLocation locationFrom, int fromIndex, InventoryLocation locationTarget,int targetIndex)
        {
            var currentList = GetItemList(locationFrom);
            var targetList = GetItemList(locationTarget);

            InventoryItem currentItem = currentList[fromIndex];

            if (targetIndex < targetList.Count)
            {
                InventoryItem targetItem = targetList[targetIndex];

                if(targetItem.itemID!=0&&currentItem.itemID!=targetItem.itemID) //有不相同的两个物品
                {
                    currentList[fromIndex] = targetItem;
                    targetList[targetIndex] = currentItem;
                }
                else if(currentItem.itemID==targetItem.itemID)   //相同的两个物品
                {
                    targetItem.itemAmount += currentItem.itemAmount;
                    targetList[targetIndex] = targetItem;
                    currentList[fromIndex] = new InventoryItem();
                }
                else  //目标空格子
                {
                    targetList[targetIndex] = currentItem;
                    currentList[fromIndex] = new InventoryItem();
                }
            }
            EventHandler.CallUpdateInventoryUI(locationFrom, currentList);
            EventHandler.CallUpdateInventoryUI(locationTarget, targetList);
        }

        /// <summary>
        /// 交易物品
        /// </summary>
        /// <param name="itemDetails">物品信息</param>
        /// <param name="amount">交易数量</param>
        /// <param name="isSellTrade">是否卖东西</param>
        public void TradeItem(ItemDetails itemDetails,int amount,bool isSellTrade)
        {
            int cost = itemDetails.itemPrice * amount;
            //获得物品背包位置
            int index = GetItemIndexInBag(itemDetails.itemID);

            //Debug.Log("进入交易流程，isSellTrade="+isSellTrade);

            if (isSellTrade)    //卖
            {
                if (playerBag.itemList[index].itemAmount >= amount)
                {
                    RemoveItem(itemDetails.itemID, amount);
                    //卖出总价
                    cost = (int)(cost * itemDetails.sellPercentage);
                    playerMoney += cost;
                }
            }
            else if (playerMoney-cost>=0)   //买
            {
                if (CheckBagCapacity())
                {
                    //Debug.Log("进入买东西流程");
                    AddItemAtIndex(itemDetails.itemID, index, amount);
                }
                playerMoney -= cost;
            }
            //刷新UI
            EventHandler.CallUpdateInventoryUI(InventoryLocation.Player,playerBag.itemList);
        }

        /// <summary>
        /// 检查建造物品资源库存
        /// </summary>
        /// <param name="ID">图纸</param>
        /// <returns></returns>
        public bool CheckStock(int ID)
        {
            var bluePrintDetails = bluePrintData.GetBluePrintDetails(ID);
            foreach(var resource in bluePrintDetails.resourceItem)
            {
                var itemStock = playerBag.GetInventoryItem(resource.itemID);
                if (itemStock.itemAmount >= resource.itemAmount)
                    continue;
                else
                    return false;
            }
            return true;
        }

        public List<InventoryItem> GetBoxDataList(string key)
        {
            if (boxDataDict.ContainsKey(key))
                return boxDataDict[key];
            return null;
        }

        /// <summary>
        /// 加入箱子数据字典
        /// </summary>
        /// <param name="box"></param>
        public void AddBoxDataDict(Box box)
        {
            var key = box.name + box.index;
            if (!boxDataDict.ContainsKey(key))
                boxDataDict.Add(key, box.boxBagData.itemList);
        }

        public GameSaveData GenerateSaveData()
        {
            GameSaveData saveData = new GameSaveData();
            saveData.playerMoney = this.playerMoney;

            saveData.inventoryDict = new Dictionary<string, List<InventoryItem>>();
            saveData.inventoryDict.Add(playerBag.name, playerBag.itemList);

            foreach(var item in boxDataDict)
            {
                saveData.inventoryDict.Add(item.Key, item.Value);
            }
            return saveData;
        }

        public void RestoreData(GameSaveData saveData)
        {
            this.playerMoney = saveData.playerMoney;
            playerBag = Instantiate(playerBagTemp);
            playerBag.itemList = saveData.inventoryDict[playerBag.name];

            foreach(var item in saveData.inventoryDict)
            {
                if (boxDataDict.ContainsKey(item.Key))
                {
                    boxDataDict[item.Key] = item.Value;
                }
            }

            EventHandler.CallUpdateInventoryUI(InventoryLocation.Player, playerBag.itemList);
        }
    }  
}
