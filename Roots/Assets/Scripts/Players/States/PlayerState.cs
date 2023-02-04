using StateMachines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Players.States {
    [System.Serializable]
    public abstract class PlayerState : State {
        public Player Player {
            get {
                if (_player == null) {
                    _player = (Player)this.Owner;
                }
                return _player;
            }
        }
        public new PlayerStateMachine StateMachine {
            get {
                if (_stateMachine == null) {
                    _stateMachine = (PlayerStateMachine)base.StateMachine;
                }
                return _stateMachine;
            }
        }

        [System.NonSerialized]
        private Player _player;
        [System.NonSerialized]
        private PlayerStateMachine _stateMachine;
    }
}