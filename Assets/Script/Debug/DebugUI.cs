using System;
using Script.Player_.StateMachineP;
using TMPro;
using UnityEngine;

namespace Script.Debug
{
    public class DebugUI : MonoBehaviour
    {
        private static DebugUI singleton;
        [SerializeField]private TextMeshProUGUI playerVelocityXDisplay;
        [SerializeField]private TextMeshProUGUI playerVelocityYDisplay;
        [SerializeField] private TextMeshProUGUI playerStateDisplay;

        public static DebugUI Singleton
        {
            get
            {
                singleton = FindObjectOfType<DebugUI>();
                return singleton;
            }
        }


        public static void DisplayVelocity(Vector2 vel)
        {
            Singleton.playerVelocityXDisplay.text = vel.x.ToString();
            Singleton.playerVelocityYDisplay.text = vel.y.ToString();
        }

        public static void DisplayState(PlayerState state)
        {
            Singleton.playerStateDisplay.text = state.Name;
        }
        
    }
}