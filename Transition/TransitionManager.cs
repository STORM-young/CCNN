using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MFarm.Save;

namespace MFarm.Transition
{
    public class TransitionManager : Singleton<TransitionManager>, ISaveable
    {
        public string startSceneName = string.Empty;

        private CanvasGroup fadeCanvasGroup;
        //�����ж��Ƿ���ɹ�������
        private bool isFade;

        public string GUID => GetComponent<DataGUID>().guid;

        protected override void Awake()
        {
            base.Awake();
            SceneManager.LoadScene("UI", LoadSceneMode.Additive);
        }

        private void OnEnable()
        {
            EventHandler.TransitionEvent += OnTransitionEvent;
            EventHandler.StartNewGameEvent += OnStartNewGameEvent;
            EventHandler.EndGameEvent += OnEndGameEvent;
        }

        private void OnDisable()
        {
            EventHandler.TransitionEvent -= OnTransitionEvent;
            EventHandler.StartNewGameEvent -= OnStartNewGameEvent;
            EventHandler.EndGameEvent -= OnEndGameEvent;
        }

        private void OnEndGameEvent()
        {
            StartCoroutine(UnloadScene());
        }

        private void OnStartNewGameEvent(int obj)
        {
            StartCoroutine(LoadSaveDataScene(startSceneName));
        }

        private void Start()
        {
            ISaveable saveable = this;
            saveable.RegisterSaveable();

            fadeCanvasGroup = FindObjectOfType<CanvasGroup>();
        }

        private void OnTransitionEvent(string sceneToGo, Vector3 positionToGo)
        {
            if (!isFade)
                StartCoroutine(Transition(sceneToGo, positionToGo));
        }

        /// <summary>
        /// �����л�
        /// </summary>
        /// <param name="sceneName">Ŀ�곡��</param>
        /// <param name="targetPosition">Ŀ��λ��</param>
        /// <returns></returns>
        private IEnumerator Transition(string sceneName, Vector3 targetPosition)
        {
            //ִ��ж�س���ǰҪ������
            EventHandler.CallBeforeSceneUnloadEvent();
            yield return Fade(1);

            yield return SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());

            //ִ�м��س�����Ҫ������
            yield return LoadSceneSetActive(sceneName);
            //�ƶ���������
            EventHandler.CallMoveToPosition(targetPosition);
            EventHandler.CallAfterSceneLoadedEvent();

            yield return Fade(0);

        }
        /// <summary>
        /// ���س���������
        /// </summary>
        /// <param name="sceneName">������</param>
        /// <returns></returns>
        private IEnumerator LoadSceneSetActive(string sceneName)
        {
            yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

            Scene newScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);

            SceneManager.SetActiveScene(newScene);
        }
        /// <summary>
        /// ���뵭������
        /// </summary>
        /// <param name="targetAlpha">1�Ǻڣ�0��͸��</param>
        /// <returns></returns>
        private IEnumerator Fade(float targetAlpha)
        {
            isFade = true;
            //�����仯�����в��ܵ�
            fadeCanvasGroup.blocksRaycasts = true;

            //ת���ٶ�
            float speed = Mathf.Abs(fadeCanvasGroup.alpha - targetAlpha) / Settings.fadeDuration;

            while (!Mathf.Approximately(fadeCanvasGroup.alpha, targetAlpha))
            {
                //�ٶȳ���ʱ������
                fadeCanvasGroup.alpha = Mathf.MoveTowards(fadeCanvasGroup.alpha, targetAlpha, speed * Time.deltaTime);
                yield return null;
            }

            fadeCanvasGroup.blocksRaycasts = false;
            isFade = false;
        }

        private IEnumerator UnloadScene()
        {
            EventHandler.CallBeforeSceneUnloadEvent();
            yield return Fade(1f);
            yield return SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
            yield return Fade(0);
        }

        private IEnumerator LoadSaveDataScene(string sceneName)
        {
            yield return Fade(1f);
            if (SceneManager.GetActiveScene().name != "PersistentScene")   //����Ϸ�����м���������Ϸ����
            {
                EventHandler.CallBeforeSceneUnloadEvent();
                yield return SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
            }  
            yield return LoadSceneSetActive(sceneName);
            EventHandler.CallAfterSceneLoadedEvent();
            yield return Fade(0);
        }

        public GameSaveData GenerateSaveData()
        {
            GameSaveData saveData = new GameSaveData();
            saveData.dataSceneName = SceneManager.GetActiveScene().name;

            return saveData;
        }

        public void RestoreData(GameSaveData saveData)
        {
            //���ض�ȡ����Ϸ����
            StartCoroutine(LoadSaveDataScene(saveData.dataSceneName));
        }
    }
}