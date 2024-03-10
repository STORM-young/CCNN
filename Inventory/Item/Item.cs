using System.Collections;
using System.Collections.Generic;
using MFarm.CropPlant;
using UnityEngine;

namespace MFarm.Inventory
{
    public class Item : MonoBehaviour
    {
        public int itemID;

        private SpriteRenderer spriteRenderer;

        public ItemDetails itemDetails;

        private BoxCollider2D coll;

        //子物体身上拿SpriteRenderer组件
        private void Awake()
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            coll = GetComponent<BoxCollider2D>();
        }

        private void Start()
        {
            if (itemID != 0)
            {
                Init(itemID);
            }
        }

        //生成这个物品
        public void Init(int ID)
        {
            itemID = ID;

            //Inventory 获取当前数据
            itemDetails = InventoryManager.Instance.GetItemDetails(itemID);

            if (itemDetails != null)
            {
                spriteRenderer.sprite = itemDetails.itemOnWorldSprite != null ? itemDetails.itemOnWorldSprite : itemDetails.itemIcon;

                //修改碰撞体尺寸
                Vector2 newSize = new Vector2(spriteRenderer.sprite.bounds.size.x, spriteRenderer.sprite.bounds.size.y);
                coll.size = newSize;
                coll.offset = new Vector2(0, spriteRenderer.sprite.bounds.center.y);
            }

            if(itemDetails.itemType == ItemType.ReapableScenery)
            {
                gameObject.AddComponent<ReapItem>();
                gameObject.GetComponent<ReapItem>().InitCropData(itemDetails.itemID);
                gameObject.AddComponent<ItemInteractive>();
            }
        }
    }
}