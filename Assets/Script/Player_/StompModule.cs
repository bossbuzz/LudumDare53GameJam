using System;
using System.Collections.Generic;
using Script.Deliverables;
using Script.Managers;
using UnityEngine;

namespace Script.Player_
{
    public class StompModule : MonoBehaviour
    {
        [SerializeField] private Player player;
        private Collider2D Collider2D;
        private TimerItemPair pair;
        [SerializeField] private ContactFilter2D CollisionFilter;
            
        private void Awake()
        {
            Collider2D = GetComponent<Collider2D>();
        }

        public void RegisterThrownItem(Deliverable deliverable,float amount)
        {
            deliverable.onBounce += remove;
            pair = new TimerItemPair(amount, deliverable);
        }

        private void remove()
        {
            if (!(pair is null))
            {
                pair.Deliverable.onBounce -= remove;
                pair = null;
                
            }
        }

        private void Update()
        {
            if (!(pair is null))
            {
                pair.Timer -= TimeManager.DeltaTime;
                if (pair.Timer <= 0) pair = null;
            }
            if (!player.controller2D.IsGrounded && player.VelocityY <= 0)
            {
                Collider2D[] results = new Collider2D[1];
                Collider2D.OverlapCollider(CollisionFilter, results);
                if (results[0])
                {
                    Deliverable deliverable =results[0].GetComponent<Deliverable>();
                    if (pair is null || deliverable != pair.Deliverable)
                    {
                        player.SetState(player.BounceStateP);
                        if (!player.GrabModule.IsCarryingObject) player.canDoubleJump = true;
                        deliverable.Stomp();
                    }
                }
            }
        }


        private class TimerItemPair
        {
            public TimerItemPair(float f, Deliverable d)
            {
                Timer = f;
                Deliverable = d;
            }
            
            public float Timer;
            public Deliverable Deliverable;
        }
    }
}