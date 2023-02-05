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

            Game.GameUIController.UpdateEnergy(Game.Player.PercentageFuel);

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
            this.Fuel = Game.GameSettings.FuelMax;
        }

        public void AddFuelDelta(int delta)
        {
            this.Fuel = Mathf.Min(Game.GameSettings.FuelMax, this.Fuel + delta);
        }

        public bool IsFuelEmpty => this.Fuel <= 0;
        public float PercentageFuel => (float)this.Fuel / Game.GameSettings.FuelMax;

        #endregion
    }
}