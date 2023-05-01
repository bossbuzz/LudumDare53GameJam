using System;
using UnityEngine;

namespace Script.Deliverables
{
    public class DestroyTimer : MonoBehaviour
    {
        [SerializeField] private float time;
        private void Update()
        {
            time -= Time.deltaTime;
            if(time <= 0) Destroy(gameObject);
        }
    }
}