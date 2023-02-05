using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachines;

namespace Players.States
{

    [System.Serializable]
    public class PlayerStateMachine : StateMachine
    {

        [SerializeField]
        private Idle _idle = null;

        [SerializeField]
        private WalkLeft _walkLeft = null;

        public Idle Idle => _idle;
        public WalkLeft WalkLeft => _walkLeft;

        protected override void RegisterStates()
        {
            this.Register(
                _idle,
                _walkLeft
                );
        }

    }
}