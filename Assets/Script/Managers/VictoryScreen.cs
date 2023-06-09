﻿using System;
using Script.Player_;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace Script.Managers
{
    public class VictoryScreen : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvas;
        private float _onGoingTime;
        [SerializeField] private float _maxTime;
        [SerializeField] private TextMeshProUGUI _minutes;
        [SerializeField] private TextMeshProUGUI _seconds;
        private InputManager _inputManager = new InputManager(true);
        public Sprite emptyStar;
        public Sprite fullStar;
        public Image[] stars;
        public TextMeshProUGUI[] _timesArray;

        private void Start()
        {
            _minutes.text = TimeManager.MinutesText;
            _seconds.text = TimeManager.SecondsText;
        }

        public void SetValues(LevelManager.minutesSeconds[] array)
        {
            int starss = 0;
            int index = 0;
            int playerSeconds = TimeManager.GetTotalSeconds;
            foreach (var ms in array)
            {
                if (ms.getSeconds() > playerSeconds) starss++;
                _timesArray[index].text = ms.minutes.ToString("00") + ":" + ms.seconds.ToString("00");
                index++;
            }
            for (int i = 0; i < starss; i++)
            {
                stars[i].sprite = fullStar;
            }
        }
        
        private void Update()
        {
            _onGoingTime += Time.deltaTime;
            if (_onGoingTime >= _maxTime)
            {
                _onGoingTime = _maxTime;
                if (_inputManager.PressedReset())
                {
                    GameMaster.ReloadLevel();
                }
                else if (_inputManager.PressedJump())
                {
                    GameMaster.gm.FinishLevel();
                }
            }
            _canvas.alpha = Mathf.Lerp(0, 1, _onGoingTime / _maxTime);
        }
    }
}