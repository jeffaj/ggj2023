using Players.States;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Players
{
    public class Player : MonoBehaviour
    {

        #region Inspector Fields

        [SerializeField]
        private PlayerStateMachine _stateMachine = null;

        [SerializeField]
        private GameSettings _gameSettings = null;

        #endregion

        public int Fuel { get; private set; }

        public PlayerStateMachine StateMachine => _stateMachine;

        public Vector3 LocalPosition
        {
            get => this.transform.localPosition;
            private set => this.transform.localPosition = value;
        }

        public Vector2Int GridPosition { get; private set; }

        public void SetGridPosition(Vector2Int gridPosition)
        {
            this.GridPosition = gridPosition;
            this.LocalPosition = Game.LevelGrid.GetLocalPosition(gridPosition);
        }

        public void IdleAt(Vector2Int gridPosition)
        {
            this.SetGridPosition(gridPosition);
            this.StateMachine.Idle.Start();
        }

        private void Awake()
        {
            _stateMachine.Awake(this);
        }

        private void Start()
        {
            _stateMachine.Idle.Start();
        }

        private void Update()
        {
            if (Game.GamePaused)
            {
                return;
            }

            _stateMachine.Update();

            Game.Instance.CheckEndConditions();

            // debug:
            UDeb.Post("state", _stateMachine.CurrentState);
            UDeb.Post("fuel %", Game.Player.PercentageFuel);
        }

        private void FixedUpdate()
        {
            if (Game.GamePaused)
            {
                return;
            }

            _stateMachine.FixedUpdate();
        }

        private void LateUpdate()
        {
            if (Game.GamePaused)
            {
                return;
            }

            _stateMachine.LateUpdate();
        }

        #region Fuel

        public void ResetFuelToFull()
        {
            this.Fuel = _gameSettings.FuelMax;
        }

        public void MoveDecrementFuel()
        {
            this.Fuel = Mathf.Max(0, this.Fuel - _gameSettings.MoveCost);
        }

        public void AddFuel(int additionalFuel)
        {
            this.Fuel = Mathf.Min(_gameSettings.FuelMax, this.Fuel + additionalFuel);
        }

        public bool IsFuelEmpty => this.Fuel <= 0;
        public float PercentageFuel => (float)this.Fuel / _gameSettings.FuelMax;

        #endregion
    }
}