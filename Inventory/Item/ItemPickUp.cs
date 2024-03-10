using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MFarm.Inventory
{
    public class ItemPickUp : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other) 
        {
            Item item = other.GetComponent<Item>();

            if(item != null)
            {
                if(item.itemDetails.canPickedup)
                {
                    //ʰȡ��Ʒ���ӵ�����
                    InventoryManager.Instance.AddItem(item, true);
                    //������Ч
                    EventHandler.CallPlaySoundEvent(SoundName.Pickup);
                }
            }
        }
    }
}
