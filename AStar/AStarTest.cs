using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

namespace MFarm.AStar
{
    public class AStarTest : MonoBehaviour
    {
        private AStar aStar;
        [Header("用于测试")]
        public Vector2Int startPos;

        public Vector2Int finishPos;
        //用来Display的Map
        public Tilemap displayMap;
        //用来绘制的Tile
        public TileBase displayTile;

        public bool displayStartAndFinsh;

        public bool displayPath;

        private Stack<MovementStep> npcMovementStepStack;

        [Header("测试移动NPC")]
        public NPCMovement npcMovement;

        public bool moveNPC;

        [SceneName] public string targetScene;

        public Vector2Int targetPos;
        public AnimationClip stopClip;

        private void Awake()
        {
            aStar = GetComponent<AStar>();
            npcMovementStepStack = new Stack<MovementStep>();
        }

        private void Update()
        {
            ShowPathOnGridMap();

            if (moveNPC)
            {
                moveNPC = false;
                var schedule = new ScheduleDetails(0, 0, 0, 0, Season.春天, targetScene, targetPos, stopClip, true);
                npcMovement.BuildPath(schedule);
            }
        }

        private void ShowPathOnGridMap()
        {
            if (displayMap != null && displayTile != null)
            {
                if (displayStartAndFinsh)
                {
                    displayMap.SetTile((Vector3Int)startPos,displayTile);
                    displayMap.SetTile((Vector3Int)finishPos, displayTile);
                }
                else
                {
                    displayMap.SetTile((Vector3Int)startPos, null);
                    displayMap.SetTile((Vector3Int)finishPos, null);
                }

                if (displayPath)
                {
                    var sceneName = SceneManager.GetActiveScene().name;

                    aStar.BuildPath(sceneName, startPos, finishPos, npcMovementStepStack);

                    foreach(var step in npcMovementStepStack)
                    {
                        displayMap.SetTile((Vector3Int)step.gridCoordinate, displayTile);
                    }
                }
                else
                {
                    if (npcMovementStepStack.Count > 0)
                    {
                        foreach(var step in npcMovementStepStack)
                        {
                            displayMap.SetTile((Vector3Int)step.gridCoordinate, null);
                        }
                        npcMovementStepStack.Clear();
                    }
                }
            }
        }
    }
}