using MFarm.Save;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MFarm.Inventory
{
    public class ItemManager : MonoBehaviour,ISaveable
    {
        public Item itemPrefab;

        public Item bounceItemPrefab;        

        private Transform itemParent;

        private Transform PlayerTransform => FindObjectOfType<Player>().transform;

        public string GUID => GetComponent<DataGUID>().guid;

        //记录场景item
        private Dictionary<string, List<SceneItem>> sceneItemDict = new Dictionary<string, List<SceneItem>>();

        private Dictionary<string, List<SceneFurniture>> sceneFunitureDict = new Dictionary<string, List<SceneFurniture>>();

        private void OnEnable()
        {
            EventHandler.InstantiateItemInScene += OnInstantiateItemInScene;
            EventHandler.DropItemEvent += OnDropItemEvent;
            EventHandler.BeforeSceneUnloadEvent += OnBeforeSceneUnloadEvent;
            EventHandler.AfterSceneLoadedEvent += OnAfterSceneLoadedEvent;
            EventHandler.BuildFurnitureEvent += OnBuildFurnitureEvent;
            EventHandler.StartNewGameEvent += OnStartNewGameEvent;
        }

        private void OnDisable()
        {
            EventHandler.InstantiateItemInScene -= OnInstantiateItemInScene;
            EventHandler.DropItemEvent -= OnDropItemEvent;
            EventHandler.BeforeSceneUnloadEvent -= OnBeforeSceneUnloadEvent;
            EventHandler.AfterSceneLoadedEvent -= OnAfterSceneLoadedEvent;
            EventHandler.BuildFurnitureEvent -= OnBuildFurnitureEvent;
            EventHandler.StartNewGameEvent -= OnStartNewGameEvent;
        }


        private void Start()
        {
            ISaveable saveable = this;
            saveable.RegisterSaveable();
        }

        private void OnStartNewGameEvent(int obj)
        {
            sceneItemDict.Clear();
            sceneFunitureDict.Clear();
        }

        private void OnBuildFurnitureEvent(int ID,Vector3 pos)
        {
            BluePrintDetails bluePrint = InventoryManager.Instance.bluePrintData.GetBluePrintDetails(ID);
            var buildItem = Instantiate(bluePrint.buildPrefab, pos, Quaternion.identity, itemParent);
            if (buildItem.GetComponent<Box>())
            {
                buildItem.GetComponent<Box>().index = InventoryManager.Instance.BoxDataAmount;
                buildItem.GetComponent<Box>().InitBox(buildItem.GetComponent<Box>().index);
            }
        }

        private void OnDropItemEvent(int ID,Vector3 mousePos,ItemType itemType)
        {
            if(itemType == ItemType.Seed)
            {
                return;
            }
            var item = Instantiate(bounceItemPrefab, PlayerTransform.position, Quaternion.identity, itemParent);
            item.itemID = ID;
            var dir = (mousePos - PlayerTransform.position).normalized;

            item.GetComponent<ItemBounce>().InitBounceItem(mousePos,dir);
        }

        private void OnBeforeSceneUnloadEvent()
        {
            GetAllSceneItems();
            GetAllSceneFurniture();
        }

        private void OnAfterSceneLoadedEvent()
        {
            itemParent = GameObject.FindWithTag("ItemParent").transform;
            RecreateAllItems();
            RebuildFurniture();
        }

        /// <summary>
        /// 在指定坐标位置生成物品
        /// </summary>
        /// <param name="ID">物品id</param>
        /// <param name="pos">世界坐标</param>
        private void OnInstantiateItemInScene(int ID,Vector3 pos)
        {
            var item = Instantiate(bounceItemPrefab, pos, Quaternion.identity, itemParent);
            item.itemID = ID;
            item.GetComponent<ItemBounce>().InitBounceItem(pos, Vector3.up*1.5f);
        }

        //收集场景物品
        /// <summary>
        /// 获取当前场景所有Item
        /// </summary>
        private void GetAllSceneItems()
        {
            List<SceneItem> currentSceneItems = new List<SceneItem>();

            foreach (var item in FindObjectsOfType<Item>()) 
            {
                SceneItem sceneItem = new SceneItem
                {
                    itemID = item.itemID,
                    position = new SerializableVector3(item.transform.position)
                };

                currentSceneItems.Add(sceneItem);
            }

            if (sceneItemDict.ContainsKey(SceneManager.GetActiveScene().name))
            {
                //找到数据就更新item数据列表
                sceneItemDict[SceneManager.GetActiveScene().name] = currentSceneItems;
            }
            else 
            {
                //如果是新场景
                sceneItemDict.Add(SceneManager.GetActiveScene().name,currentSceneItems);
            }
        }

        /// <summary>
        /// 刷新重建当前场景物品
        /// </summary>
        private void RecreateAllItems()
        {
            List<SceneItem> currentSceneItems = new List<SceneItem>();
            //尝试寻找当前激活的场景有没有数据，如果有就out返回
            if (sceneItemDict.TryGetValue(SceneManager.GetActiveScene().name, out currentSceneItems))
            {
                //保存的数据不为空则循环创建
                if(currentSceneItems != null)
                {
                    //清场
                    foreach(var item in FindObjectsOfType<Item>())
                    {
                        Destroy(item.gameObject);
                    }
                    
                    foreach(var item in currentSceneItems)
                    {
                        Item newItem = Instantiate(itemPrefab, item.position.ToVector3(), Quaternion.identity, itemParent); 
                        newItem.Init(item.itemID);
                    }
                }
            }
        }

        /// <summary>
        /// 获得场景所有家具
        /// </summary>
        private void GetAllSceneFurniture()
        {
            List<SceneFurniture> currentSceneFurniture = new List<SceneFurniture>();

            foreach (var item in FindObjectsOfType<Furniture>())
            {
                SceneFurniture sceneFurniture = new SceneFurniture
                {
                    itemID = item.itemID,
                    position = new SerializableVector3(item.transform.position)
                };

                if (item.GetComponent<Box>())
                    sceneFurniture.boxIndex = item.GetComponent<Box>().index;

                currentSceneFurniture.Add(sceneFurniture);
            }

            if (sceneFunitureDict.ContainsKey(SceneManager.GetActiveScene().name))
            {
                //找到数据就更新item数据列表
                sceneFunitureDict[SceneManager.GetActiveScene().name] = currentSceneFurniture;
            }
            else
            {
                //如果是新场景
                sceneFunitureDict.Add(SceneManager.GetActiveScene().name, currentSceneFurniture);
            }
        }

        /// <summary>
        /// 重建当前场景家具
        /// </summary>
        private void RebuildFurniture()
        {
            List<SceneFurniture> currentSceneFurniture = new List<SceneFurniture>();

            if (sceneFunitureDict.TryGetValue(SceneManager.GetActiveScene().name, out currentSceneFurniture))
            {
                if (currentSceneFurniture != null)
                {
                    foreach (SceneFurniture sceneFurniture in currentSceneFurniture)
                    {
                        BluePrintDetails bluePrint = InventoryManager.Instance.bluePrintData.GetBluePrintDetails(sceneFurniture.itemID);
                        var buildItem = Instantiate(bluePrint.buildPrefab, sceneFurniture.position.ToVector3(), Quaternion.identity, itemParent);
                        if (buildItem.GetComponent<Box>())
                        {
                            buildItem.GetComponent<Box>().InitBox(sceneFurniture.boxIndex);
                        }
                           
                    }
                }
            }
        }

        public GameSaveData GenerateSaveData()
        {
            GetAllSceneFurniture();
            GetAllSceneItems();

            GameSaveData saveData = new GameSaveData();
            saveData.sceneFunitureDict = this.sceneFunitureDict;
            saveData.sceneItemDict = this.sceneItemDict;

            return saveData;
        }

        public void RestoreData(GameSaveData saveData)
        {
            this.sceneItemDict = saveData.sceneItemDict;
            this.sceneFunitureDict = saveData.sceneFunitureDict;

            RecreateAllItems();
            RebuildFurniture();
        }
    }

}