using System;
using Script.Deliverables;
using Script.Managers;
using UnityEngine;

namespace Script.Player_
{
    [Serializable]
    public class GrabModule
    {
        [SerializeField] private BoxCollider2D Collider2D;
        [SerializeField] public ContactFilter2D CollisionFilter;
        [SerializeField] public Transform GrabPivot;
        [SerializeField] public Player player;
        public float throwStompTimer = 0.5f;
        private Deliverable _carriedObject;

        public bool IsCarryingObject => !(_carriedObject is null);
        
        public void Grab()
        {
            if (_carriedObject is null)
            {
                Deliverable objectToGrab = GetDeliverable();
                if (objectToGrab is null) return;
                _carriedObject = objectToGrab;
                _carriedObject.Pickup(GrabPivot);
            }
            else
            {
                _carriedObject.Drop(player.transform.position);
                if (!player.controller2D.IsGrounded) player.canDoubleJump = false;
                player.StompModule.RegisterThrownItem(_carriedObject,throwStompTimer);
                _carriedObject = null;
            }
        }

        public void Throw(Vector2 direction)
        {
            if (_carriedObject is null)
            {
                Grab();
                return;
            }
            if (!player.controller2D.IsGrounded) player.canDoubleJump = false;
            player.Animator.SetTrigger("throw");
            AudioManager.PlayClip(AudioManager.ThrowClip,1,2);
            player.StompModule.RegisterThrownItem(_carriedObject,throwStompTimer);
            _carriedObject.Throw(direction,player.transform);
            _carriedObject = null;
        }
        
        public Deliverable GetDeliverable()
        {
            Collider2D[] results = new Collider2D[3];
            Collider2D.OverlapCollider(CollisionFilter, results);
            if (results[0])
            {
                return results[0].gameObject.GetComponent<Deliverable>();
            }
            return null;
        }
        
    }
}