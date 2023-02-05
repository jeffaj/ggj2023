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

        [SerializeField]
        private BreakBlockLeft _breakBlockLeft = null;

        [SerializeField]
        private BreakBlockDown _breakBlockDown = null;

        [SerializeField]
        private BreakBlockRight _breakBlockRight = null;

        [SerializeField]
        private OutOfFuel _outOfFuel = null;

        public Idle Idle => _idle;
        public WalkLeft WalkLeft => _walkLeft;
        public BreakBlockDown BreakBlockDown => _breakBlockDown;
        public OutOfFuel OutOfFuel => _outOfFuel;
        public BreakBlockLeft BreakBlockLeft => _breakBlockLeft;
        public BreakBlockRight BreakBlockRight => _breakBlockRight;

        protected override void RegisterStates()
        {
            this.Register(
                _idle,
                _walkLeft,
                _breakBlockDown,
                _outOfFuel,
                _breakBlockLeft,
                _breakBlockRight
                );
        }
    }
}