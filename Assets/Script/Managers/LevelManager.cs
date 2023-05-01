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
        public minutesSeconds[] times = new minutesSeconds[3];
        
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
                if (!receptor.Complete) return;
            }
            TimeManager.Pause(true);
            TimeManager.TM.ShowTimer(false);
            _victoryScreen.SetActive(true);
            _victoryScreen.GetComponent<VictoryScreen>().SetValues(times);
            AudioManager.PlayClip(AudioManager.FinishLevelClip);
            Player player = FindObjectOfType<Player>();
            if(player) player.SetState(player.CelebStateP);
            enabled = false;
            
        }

        [Serializable]
        public struct minutesSeconds
        {
            [SerializeField] public int minutes;
            [SerializeField] public int seconds;

            public int getSeconds()
            {
                return seconds + (minutes * 60);
            }
        }
    }
}