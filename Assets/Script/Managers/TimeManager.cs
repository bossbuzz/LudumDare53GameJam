using System;
using System.Timers;
using TMPro;
using UnityEditor.Build;
using UnityEngine;

namespace Script.Managers
{
    public class TimeManager : MonoBehaviour
    {
        private static TimeManager _tm;
        private bool pause = false;
        [SerializeField] private TextMeshProUGUI _textMinutes;
        [SerializeField] private TextMeshProUGUI _textSeconds;
        [SerializeField] private GameObject parentText;
        private int minutes;
        private int seconds;
        private float time;
        private void Awake()
        {
            if (_tm is null)
            {
                _tm = this;
            }
            else Destroy(this);
        }

        public static int Minutes
        {
            get => TM.minutes;
        }
        
        public static int Seconds
        {
            get => TM.seconds;
        }

        public static int GetTotalSeconds
        {
            get => TM.seconds + (TM.minutes * 60);
        }
        
        public static string MinutesText
        {
            get => TM._textMinutes.text;
        }
        
        public static string SecondsText
        {
            get => TM._textSeconds.text;
        }
        
        public static TimeManager TM
        {
            get
            {
                if (_tm is null)
                {
                    _tm = FindObjectOfType<TimeManager>();
                }
                return _tm;
            }
        }

        public static bool IsPaused => DeltaTime == 0;
        
        public static float DeltaTime
        {
            get
            {
                if (TM.pause) return 0;
                return Time.deltaTime;
            }
        }

        public static void Pause(bool isPaused)
        {
            TM.pause = isPaused;
        }

        private void Update()
        {
            time += DeltaTime;
            minutes = (int) time / 60;
            seconds = (int) time % 60;
            _textMinutes.SetText(minutes.ToString("00"));
            _textSeconds.SetText(seconds.ToString("00"));
        }

        public void ResetTimer()
        {
            time = 0;
        }
        
        public void ShowTimer(bool show)
        {
            parentText.SetActive(show);
        }
    }
}