using System;
using System.Collections.Generic;
using UnityEngine;

namespace Script.Deliverables
{
    public class Receptor : MonoBehaviour
    {
        [SerializeField] private Collider2D _collider2D;
        [SerializeField] private ContactFilter2D _filter;
        private List<IDeliverableId> _deliverables;
        [SerializeField] protected Deliverable _requestedDeliverable;
        protected bool _complete;
        private readonly Dictionary<Collider2D, IDeliverableId> _dictionary = new Dictionary<Collider2D, IDeliverableId>();
        [SerializeField] private SpriteRenderer _signSprite;
        [SerializeField] protected Animator _basketAnimator;
        
        private void Awake()
        {
            _signSprite.sprite = _requestedDeliverable.sprite;
        }
        
        public virtual bool Complete
        {
            get
            {
                return _complete;
            }
            set
            {
                _complete = value;
                _basketAnimator.SetBool("complete",_complete);
            }
        }
        private void Update()
        {
            Collider2D[] results = new Collider2D[10];
            _collider2D.OverlapCollider(_filter, results);
            foreach (var col in results)
            {
                if (col is null)
                {
                    Register(false);
                    continue;
                }
                if (!_dictionary.ContainsKey(col))
                {
                    _dictionary.Add(col,col.GetComponent<IDeliverableId>());
                }
                bool found = _dictionary[col].Id == _requestedDeliverable.Id;
                Register(found);
                if (found) return;   
            }
        }

        private void Register(bool complete)
        {
            Complete = complete;
        }
    }
}