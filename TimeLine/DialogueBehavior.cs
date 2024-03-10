using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using MFarm.Dialogue;

[System.Serializable]
public class DialogueBehavior : PlayableBehaviour
{
    private PlayableDirector director;

    public DialougePiece dialoguePiece;

    public override void OnPlayableCreate(Playable playable)
    {
        director = (playable.GetGraph().GetResolver() as PlayableDirector);
    }

    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        //呼叫启动UI
        EventHandler.CallShowDialogueEvent(dialoguePiece);
        if (Application.isPlaying)
        {
            if (dialoguePiece.hasToPause)
            {
                //暂停TimeLine
                TimelineManager.Instance.PauseTimeline(director);
            }
            else
            {
                EventHandler.CallShowDialogueEvent(null);
            }
        }
    }

    //在TimeLine播放期间每帧执行
    public override void ProcessFrame(Playable playable,FrameData info,object playerData)
    {
        if (Application.isPlaying)
            TimelineManager.Instance.IsDone = dialoguePiece.isDone;
    }

    public override void OnBehaviourPause(Playable playable,FrameData info)
    {
        EventHandler.CallShowDialogueEvent(null);
    }

    public override void OnGraphStart(Playable playable)
    {
        EventHandler.CallUpdateGameStateEvent(GameState.Pause);
    }

    public override void OnGraphStop(Playable playable)
    {
        EventHandler.CallUpdateGameStateEvent(GameState.Gameplay);
        if(TimelineManager.Instance!=null)
            TimelineManager.Instance.startDirector.gameObject.SetActive(false);
    }
}
