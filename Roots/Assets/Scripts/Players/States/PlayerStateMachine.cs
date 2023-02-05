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
        private WalkRight _walkRight = null;

        [SerializeField]
        private BreakBlockLeft _breakBlockLeft = null;

        [SerializeField]
        private BreakBlockDown _breakBlockDown = null;

        [SerializeField]
        private BreakBlockRight _breakBlockRight = null;

        [SerializeField]
        private BreakBlockLeftFail _breakBlockLeftFail = null;

        [SerializeField]
        private BreakBlockRightFail _breakBlockRightFail = null;

        [SerializeField]
        private BreakBlockDownFail _breakBlockDownFail = null;

        [SerializeField]
        private OutOfFuel _outOfFuel = null;
        [SerializeField]
        private GrabbedByRoot _grabbedByRoot = null;

        public Idle Idle => _idle;
        public WalkLeft WalkLeft => _walkLeft;
        public WalkRight WalkRight => _walkRight;
        public BreakBlockDown BreakBlockDown => _breakBlockDown;
        public OutOfFuel OutOfFuel => _outOfFuel;
        public GrabbedByRoot GrabbedByRoot => _grabbedByRoot;
        public BreakBlockLeft BreakBlockLeft => _breakBlockLeft;
        public BreakBlockRight BreakBlockRight => _breakBlockRight;
        public BreakBlockLeftFail BreakBlockLeftFail => _breakBlockLeftFail;
        public BreakBlockRightFail BreakBlockRightFail => _breakBlockRightFail;
        public BreakBlockDownFail BreakBlockDownFail => _breakBlockDownFail;

        protected override void RegisterStates()
        {
            this.Register(
                _idle,
                _walkLeft,
                _breakBlockDown,
                _outOfFuel,
                _grabbedByRoot,
                _breakBlockLeft,
                _breakBlockRight,
                _breakBlockLeftFail,
                _breakBlockRightFail,
                _walkRight,
                _breakBlockDownFail
                );
        }
    }
}