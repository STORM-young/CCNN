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
            dataTime.text = "������绹û��ʼ";
            dataScene.text = "�λ�û��ʼ";
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
            Debug.Log("����Ϸ");
            EventHandler.CallStartNewGameEvent(Index);
        }
    }
}
