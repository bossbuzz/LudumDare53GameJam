using System;
using Script.Player_;
using Unity.VisualScripting;
using UnityEngine;

namespace Script.Managers
{
    public class TitleScreen : MonoBehaviour
    {
        private InputManager _inputManager = new InputManager(true);

        private void Start()
        {
            AudioManager.PlaySong(AudioManager.TitleMusic);
        }

        private void Update()
        {
            if (_inputManager.PressedJump())
            {
                AudioManager.PlaySong(AudioManager.LevelMusic);
                GameMaster.gm.FinishLevel();
                Destroy(gameObject);
            }
        }
    }
}