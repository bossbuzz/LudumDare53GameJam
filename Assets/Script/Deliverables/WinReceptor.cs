using System;
using UnityEngine;

namespace Script.Deliverables
{
    public class WinReceptor : Receptor
    {
        [SerializeField] private Animator _flagAnimator;

        public override bool Complete
        {
            get => _complete;

            set
            {
                _complete = value;
                _basketAnimator.SetBool("complete",_complete);
                _flagAnimator.SetBool("complete",_complete);
            }
        }
    }
}