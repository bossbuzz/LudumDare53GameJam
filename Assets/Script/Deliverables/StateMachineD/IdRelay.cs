using UnityEngine;

namespace Script.Deliverables.StateMachineD
{
    public class IdRelay : MonoBehaviour,IDeliverableId
    {
        [SerializeField] private int id;
        public int Id => id;
    }
}