using Players.States;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Players {
    public class Player : MonoBehaviour {

        #region Inspector Fields

        [SerializeField]
        private PlayerStateMachine _stateMachine = null;

        #endregion

        public PlayerStateMachine StateMachine => _stateMachine;

        public Vector3 Velocity {
            get => _rigidBody.velocity;
            set => _rigidBody.velocity = value;
        }

        private void Awake() {
            _rigidBody = this.GetComponent<Rigidbody>();
            _stateMachine.Awake(this);
        }

        private void Start() {
            _stateMachine.Idle.Start();
        }

        private void Update() {

            _stateMachine.Update();

            // debug:
            UDeb.Post("state", _stateMachine.CurrentState);
            UDeb.Post("vx", this.Velocity.x);
            UDeb.Post("vy", this.Velocity.y);
        }

        private void FixedUpdate() {

            _stateMachine.FixedUpdate();

        }

        private void LateUpdate() {

            _stateMachine.LateUpdate();

        }

        private Rigidbody _rigidBody;
    }
}