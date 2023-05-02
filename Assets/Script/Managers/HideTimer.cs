using System;
using UnityEngine;

namespace Script.Managers
{
    public class HideTimer : MonoBehaviour
    {
        private void Start()
        {
            TimeManager.TM.ShowTimer(false);
        }
    }
}