 using System.Collections;
using System.Collections.Generic;
using MFarm.Save;
using UnityEngine;
using UnityEngine.UI;

public class SaveSlotUI : MonoBehaviour
{
    public Text dataTime, dataScene;

    private Button currentButton;

    private int Index => transform.GetSiblingIndex();

    private DataSlot currentDataSlot;

    private void Awake()
    {
        currentButton = GetComponent<Button>();
        currentButton.onClick.AddListener(LoadGameData);
    }

    private void OnEnable()
    {
        SetupSlotUI();
    }

    private void SetupSlotUI()
    {
        currentDataSlot = SaveLoadManager.Instance.dataSlots[Index];

        if (currentDataSlot != null)
        {
            dataTime.text = currentDataSlot.DataTime;
            dataScene.text = currentDataSlot.DataScene;
        }
        else
        {
            dataTime.text = "这个世界还没开始";
            dataScene.text = "梦还没开始";
        }
    }

    private void LoadGameData()
    {
        if (currentDataSlot != null)
        {
            SaveLoadManager.Instance.Load(Index);
        }
        else
        {
            Debug.Log("新游戏");
            EventHandler.CallStartNewGameEvent(Index);
        }
    }
}
