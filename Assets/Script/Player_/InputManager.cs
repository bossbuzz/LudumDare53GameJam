using Script.Managers;
using UnityEngine;

namespace Script.Player_
{
    public class InputManager
    {
        private readonly bool _ignorePause = false;
        private KeyCode KeyUp => KeyCode.UpArrow;
        private KeyCode KeyDown => KeyCode.DownArrow;
        private KeyCode KeyLeft => KeyCode.LeftArrow;
        private KeyCode KeyRight => KeyCode.RightArrow;
        private KeyCode KeyJump => KeyCode.Z;
        private KeyCode KeyGrab => KeyCode.X;
        private KeyCode KeyThrow => KeyCode.C;
        private KeyCode KeyReset => KeyCode.R;
        
        public Vector2 DirectionalInput()
        {
            if (TimeManager.IsPaused && !_ignorePause) return Vector2.zero;
            Vector2 vector = new Vector2();
            if (Input.GetKey(KeyUp)) vector.y = 1;
            if (Input.GetKey(KeyDown)) vector.y = -1;
            if (Input.GetKey(KeyLeft)) vector.x = -1;
            if (Input.GetKey(KeyRight)) vector.x = 1;
            if (vector != Vector2.zero)
            {
                int x = 0;
            }
            return vector;
        }

        public bool PressedJump()
        {
            return Input.GetKeyDown(KeyJump) && (!TimeManager.IsPaused || _ignorePause);
        }

        public bool PressedGrab()
        {
            return Input.GetKeyDown(KeyGrab)&& (!TimeManager.IsPaused || _ignorePause);
        }

        public bool PressedThrow()
        {
            return Input.GetKeyDown(KeyThrow)&& (!TimeManager.IsPaused || _ignorePause);
        }

        public bool PressedReset()
        {
            return Input.GetKeyDown(KeyReset)&& (!TimeManager.IsPaused || _ignorePause);
        }

        public InputManager(bool ignorePause)
        {
            _ignorePause = ignorePause;
        }
        
    }
}