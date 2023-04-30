using System;
using System.Collections.Generic;
using Script.Debug;
using Script.Deliverables;
using UnityEngine;

namespace Script.Managers
{
    public class LevelManager : MonoBehaviour
    {
        private readonly List<Receptor> _receptors = new List<Receptor>();
        private void Start()
        {
            foreach (var receptor in FindObjectsOfType<WinReceptor>())
            {
                _receptors.Add(receptor);
            }
        }

        private void Update()
        {
            foreach (var receptor in _receptors)
            {
                if (!receptor._complete) return;
            }
            GameMaster.gm.FinishLevel();
        }
    }
}