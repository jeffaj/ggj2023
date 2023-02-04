using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachines;

namespace Players.States {

    [System.Serializable]
    public class PlayerStateMachine : StateMachine {

        [SerializeField]
        private Idle _idle = null;

        public Idle Idle => _idle;

        protected override void RegisterStates() {
            this.Register(
                _idle
                );
        }

    }
}