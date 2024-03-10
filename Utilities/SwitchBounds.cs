using Cinemachine;
using UnityEngine;

public class SwitchBounds : MonoBehaviour
{
    //TODO:切换场景后更改调用
    // Start is called before the first frame update
    private void start()
    {
        SwitchConfinerShape();
    }

    private void OnEnable()
    {
        EventHandler.AfterSceneLoadedEvent += SwitchConfinerShape;
    }

    private void OnDisable()
    {
        EventHandler.AfterSceneLoadedEvent -= SwitchConfinerShape;
    }
    
    private void SwitchConfinerShape() 
    {
        PolygonCollider2D confinerShape = GameObject.FindGameObjectWithTag("BoundsConfiner").GetComponent<PolygonCollider2D>();
        CinemachineConfiner confiner = GetComponent<CinemachineConfiner>();
        confiner.m_BoundingShape2D = confinerShape;

        //Call this if the bounding shape's points change at runtime
        confiner.InvalidatePathCache();
    }
}
