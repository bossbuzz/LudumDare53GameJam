using System;
using System.Collections.Generic;
using Script.Player_;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Script.Managers
{
    public class GameMaster : MonoBehaviour
    {
        public static GameMaster gm;
        [SerializeField] private SceneAsset[] Scenes = new SceneAsset[20];
        private InputManager _inputManager = new InputManager(true);
        public bool dontLoadFromList;
        private int index;

        private void Awake()
        {
            if (gm is null)
            {
                DontDestroyOnLoad(this);
                gm = this;
                index = -1;
                SceneManager.sceneLoaded += ManageScenes;
            }
            else Destroy(this);
        }
        
        public void FinishLevel()
        {
            LoadNextLevel();
            TimeManager.Pause(false);
            TimeManager.TM.ResetTimer();
            TimeManager.TM.ShowTimer(true);
        }
        
        private void LoadNextLevel()
        {
            index++;
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
            SceneManager.LoadScene(Scenes[index].name);
        }

        public static void ReloadLevel()
        {
            Scene CurrentScene = SceneManager.GetActiveScene();
            SceneManager.UnloadSceneAsync(CurrentScene);
            SceneManager.LoadScene(CurrentScene.name);
            TimeManager.Pause(false);
            TimeManager.TM.ResetTimer();
            TimeManager.TM.ShowTimer(true);
        }
        
        private void ManageScenes(Scene s, LoadSceneMode mode)
        {
        }
    }
}