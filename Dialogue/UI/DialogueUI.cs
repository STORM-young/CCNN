using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using MFarm.Dialogue;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{
    public GameObject dialogueBox;

    public Text dialogueText;

    public Image faceRight, faceLeft;

    public Text nameRight, nameLeft;

    public GameObject continueBox;

    private void Awake()
    {
        continueBox.SetActive(false);
    }

    private void OnEnable()
    {
        EventHandler.ShowDialogueEvent += OnShowDialogueEvent;
    }

    private void OnDisable()
    {
        EventHandler.ShowDialogueEvent -= OnShowDialogueEvent;
    }

    private void OnShowDialogueEvent(DialougePiece piece)
    {
        StartCoroutine(ShowDialogue(piece));
    }

    private IEnumerator ShowDialogue(DialougePiece piece)
    {
        if (piece != null)
        {
            piece.isDone = false;

            dialogueBox.SetActive(true);
            continueBox.SetActive(false);

            dialogueText.text = string.Empty;

            if (piece.name != string.Empty)
            {
                if(piece.onLeft)
                {
                    faceRight.gameObject.SetActive(false);
                    faceLeft.gameObject.SetActive(true);
                    faceLeft.sprite = piece.faceImage;
                    nameLeft.text = piece.name;
                }
                else
                {
                    faceRight.gameObject.SetActive(true);
                    faceLeft.gameObject.SetActive(false);
                    faceRight.sprite = piece.faceImage;
                    nameRight.text = piece.name;
                }
            }
            else
            {
                faceRight.gameObject.SetActive(false);
                faceLeft.gameObject.SetActive(false);
                nameRight.gameObject.SetActive(false);
                nameLeft.gameObject.SetActive(false);
            }
            yield return dialogueText.DOText(piece.dialogueText, 1f).WaitForCompletion();

            piece.isDone = true;

            if (piece.hasToPause && piece.isDone)
                continueBox.SetActive(true);
        }
        else
        {
            dialogueBox.SetActive(false);
            yield break;
        }
    }
}
