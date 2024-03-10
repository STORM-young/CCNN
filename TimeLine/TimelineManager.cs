using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineManager : Singleton<TimelineManager>
{
    public PlayableDirector startDirector;
    
    private PlayableDirector currentDirector;

    private bool isPause;

    private bool isDone;

    private bool newGame;

    public bool IsDone { set => isDone = value; }

    private void OnEnable()
    {
        EventHandler.StartNewGameEvent += OnStartNewGameEvent;
        EventHandler.AfterSceneLoadedEvent += OnAfterSceneLoadedEvent;
    }

    private void OnDisable()
    {
        EventHandler.StartNewGameEvent -= OnStartNewGameEvent;
        EventHandler.AfterSceneLoadedEvent -= OnAfterSceneLoadedEvent;
    }


    private void OnStartNewGameEvent(int obj)
    {
        newGame = true;
        //currentDirector.gameObject.SetActive(false);
    }

    private void OnAfterSceneLoadedEvent()
    {
        currentDirector = FindObjectOfType<PlayableDirector>();
        if (currentDirector != null && newGame)
        {
            currentDirector.Play();
            newGame = false;
        }
        //currentDirector.gameObject.SetActive(false);
    }

    protected override void Awake()
    {
        base.Awake();
        currentDirector = startDirector;
    }

    private void Update()
    {
        if(isPause && Input.GetKeyDown(KeyCode.Space) && isDone)
        {
            isPause = false;
            currentDirector.playableGraph.GetRootPlayable(0).SetSpeed(1d);
        }
    }

    public void PauseTimeline(PlayableDirector director)
    {
        currentDirector = director;

        currentDirector.playableGraph.GetRootPlayable(0).SetSpeed(0d);
        isPause = true;
    }

    private void TimelineStopped(PlayableDirector director)
    {
        if (director != null)
        {
            EventHandler.CallUpdateGameStateEvent(GameState.Gameplay);
            director.gameObject.SetActive(false);
        }
    }
}
