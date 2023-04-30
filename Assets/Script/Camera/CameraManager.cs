using System;
using Script.Player_;
using UnityEngine;

namespace Script.Camera
{
    public class CameraManager : MonoBehaviour
    {
        private Transform _follow;

        private void Awake()
        {
            _follow = FindObjectOfType<Player>().transform;
        }

        private void Update()
        {
            Vector3 newPosition = _follow.position;
            var transform1 = transform;
            newPosition.z = transform1.position.z;
            transform1.position = newPosition;
        }
    }
}