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

        public Vector3 LocalPosition {
            get => this.transform.localPosition;
            private set => this.transform.localPosition = value;
        }

        public Vector2Int GridPosition { get; private set; }

        public void SetGridPosition(Vector2Int gridPosition) {
            this.GridPosition = gridPosition;
            this.LocalPosition = Game.LevelGrid.GetLocalPosition(gridPosition);
        }

        public void IdleAt(Vector2Int gridPosition) {
            this.SetGridPosition(gridPosition);
            this.StateMachine.Idle.Start();
        }

        private void Awake() {
            _stateMachine.Awake(this);
        }

        private void Start() {
            _stateMachine.Idle.Start();
        }

        private void Update() {

            _stateMachine.Update();

            // debug:
            UDeb.Post("state", _stateMachine.CurrentState);
            UDeb.Post("fuel %", Game.PlayerFuel.Percentage);
        }

        private void FixedUpdate() {

            _stateMachine.FixedUpdate();

        }

        private void LateUpdate() {

            _stateMachine.LateUpdate();

        }

    }
}