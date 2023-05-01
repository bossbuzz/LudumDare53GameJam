using System;
using System.Collections.Generic;
using Script.Debug;
using Script.Deliverables;
using Script.Player_;
using UnityEngine;

namespace Script.Managers
{
    public class LevelManager : MonoBehaviour
    {
        private readonly List<Receptor> _receptors = new List<Receptor>();
        private InputManager _inputManager = new InputManager(true);
        [SerializeField] private GameObject _victoryScreen;
        private void Start()
        {
            foreach (var receptor in FindObjectsOfType<WinReceptor>())
            {
                _receptors.Add(receptor);
            }
        }

        private void Update()
        {
            if (_inputManager.PressedReset())
            {
                GameMaster.ReloadLevel();
                return;
            }
            foreach (var receptor in _receptors)
            {
                if (!receptor._complete) return;
                TimeManager.Pause(true);
                TimeManager.TM.ShowTimer(false);
                _victoryScreen.SetActive(true);
                enabled = false;
            }
            
        }
    }
}