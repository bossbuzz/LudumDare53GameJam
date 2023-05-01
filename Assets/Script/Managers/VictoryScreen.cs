using System;
using Script.Player_;
using TMPro;
using UnityEngine;

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
        private void Start()
        {
            _minutes.text = TimeManager.Minutes;
            _seconds.text = TimeManager.Seconds;
        }

        private void Update()
        {
            _onGoingTime += Time.deltaTime;
            if (_onGoingTime >= _maxTime)
            {
                _onGoingTime = _maxTime;
                if (_inputManager.PressedJump())
                {
                    GameMaster.gm.FinishLevel();
                }
            }
            _canvas.alpha = Mathf.Lerp(0, 1, _onGoingTime / _maxTime);
        }
    }
}