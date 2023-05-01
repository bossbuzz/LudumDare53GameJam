using System;
using Unity.Mathematics;
using UnityEngine;

namespace Script.Deliverables
{
    public class Dispenser : MonoBehaviour
    {
        [SerializeField] private GameObject deliverable;
        private Deliverable activeObject;
        [SerializeField] private Transform spawnPivot;
        private void Start()
        {
            if (activeObject is null)
            {
                resupply();
            }
        }

        private void resupply()
        {
            activeObject = Instantiate(deliverable, spawnPivot.position, quaternion.identity).GetComponent<Deliverable>();
            activeObject.onDestory += resupply;
        }
    }
}