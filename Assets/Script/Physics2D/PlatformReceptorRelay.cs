using System;
using Script.Deliverables;
using UnityEngine;

namespace Script.Physics2D
{
    public class PlatformReceptorRelay : MonoBehaviour
    {
        public Receptor Receptor;
        public PlatformController platform;
        public float speed;

        private void Update()
        {
            if (Receptor.Complete)
            {
                platform.speed = speed;
            }
            else platform.speed = 0;
        }
    }
}