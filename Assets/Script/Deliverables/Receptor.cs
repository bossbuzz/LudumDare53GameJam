using System;
using System.Collections.Generic;
using UnityEngine;

namespace Script.Deliverables
{
    public class Receptor : MonoBehaviour
    {
        [SerializeField] private Collider2D _collider2D;
        [SerializeField] private ContactFilter2D _filter;
        [SerializeField] private SpriteRenderer _renderer;
        private List<IDeliverableId> _deliverables;
        [SerializeField] private int _requestedId;
        [SerializeField] public bool _complete;
        private readonly Dictionary<Collider2D, IDeliverableId> _dictionary = new Dictionary<Collider2D, IDeliverableId>();

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
                bool found = _dictionary[col].Id == _requestedId;
                Register(found);
                if (found) return;   
            }
        }

        private void Register(bool complete)
        {
            _complete = complete;
            if(!_complete) _renderer.color = Color.red;
            else
            {
                _renderer.color = Color.green;
            }
        }
    }
}