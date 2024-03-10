using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MFarm.Dialogue
{
    [RequireComponent(typeof(NPCMovement))]
    [RequireComponent(typeof(BoxCollider2D))]
    public class DialogueController : MonoBehaviour
    {
        private NPCMovement npc => GetComponent<NPCMovement>();

        public UnityEvent OnFinishEvent;

        public List<DialougePiece> dialougeList = new List<DialougePiece>();

        private Stack<DialougePiece> dialogueStack;

        private bool canTalk;

        private GameObject uiSign;

        private bool isTalking;

        private void Awake()
        {
            uiSign = transform.GetChild(1).gameObject;
            FillDialogueStack();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                canTalk = !npc.isMoving && npc.interactable;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                canTalk = false;
            }
        }

        private void Update()
        {
            uiSign.SetActive(canTalk);

            if (canTalk && Input.GetKeyDown(KeyCode.Space) && !isTalking)
            {
                StartCoroutine(DialogueRoutine());
            }
        }

        /// <summary>
        /// 构建对话堆栈
        /// </summary>
        private void FillDialogueStack()
        {
            dialogueStack = new Stack<DialougePiece>();
            for(int i = dialougeList.Count - 1; i >=0; i--)
            {
                dialougeList[i].isDone = false;
                dialogueStack.Push(dialougeList[i]);
            }
        }

        private IEnumerator DialogueRoutine()
        {
            isTalking = true;
            if (dialogueStack.TryPop(out DialougePiece result))
            {
                //传到UI显示对话
                EventHandler.CallShowDialogueEvent(result);
                EventHandler.CallUpdateGameStateEvent(GameState.Pause);
                yield return new WaitUntil(() => result.isDone);
                isTalking = false;
            }
            else
            {
                EventHandler.CallShowDialogueEvent(null);
                EventHandler.CallUpdateGameStateEvent(GameState.Gameplay);
                FillDialogueStack();
                isTalking = false;
                if (OnFinishEvent != null)
                {
                    OnFinishEvent?.Invoke();
                    canTalk = false;
                }
                
            }
        }
    }
}
