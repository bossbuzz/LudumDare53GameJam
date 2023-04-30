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
        private InputManager _inputManager = new InputManager();
        private int index;

        private void Awake()
        {
            DontDestroyOnLoad(this);
            gm = this;
            index = -1;
            SceneManager.sceneLoaded += ManageScenes;
        }

        private void Start()
        {
            LoadNextLevel();
        }

        private void Update()
        {
            if (_inputManager.PressedReset())
            {
                ReloadLevel();
            }
        }

        public void FinishLevel()
        {
            LoadNextLevel();
        }
        
        private void LoadNextLevel()
        {
            index++;
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
            SceneManager.LoadScene(Scenes[index].name);
        }

        private void ReloadLevel()
        {
            Scene CurrentScene = SceneManager.GetActiveScene();
            SceneManager.UnloadSceneAsync(CurrentScene);
            SceneManager.LoadScene(CurrentScene.name);
        }
        
        private void ManageScenes(Scene s, LoadSceneMode mode)
        {
        }
    }
}