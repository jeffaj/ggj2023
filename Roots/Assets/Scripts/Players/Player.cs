using Players.States;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEditor.Experimental.GraphView.GraphView;

namespace Players
{
    public class Player : MonoBehaviour
    {

        #region Inspector Fields

        [SerializeField]
        private PlayerStateMachine _stateMachine = null;

        [SerializeField]
        public Animator AnimationController = null;

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

        // moves player to the given grid location, then sets state
        // to Idle, using a coroutine.
        public void LerpToIdle(Vector2Int endGridPos, float duration, UnityAction lerpDone)
        {
            StartCoroutine(LerpToIdleCoroutine(endGridPos, duration, lerpDone));
        }

        private IEnumerator LerpToIdleCoroutine(Vector2Int endGridPos, float duration, UnityAction lerpDone)
        {
            Vector2Int startGridPos = GridPosition;

            Vector3 startPosLocal = Game.LevelGrid.GetLocalPosition(startGridPos);
            Vector3 endPosLocal = Game.LevelGrid.GetLocalPosition(endGridPos);

            float startTime = Time.time;

            while (startTime + duration > Time.time)
            {
                float prog = (Time.time - startTime) / duration;

                var position = Vector3.Lerp(startPosLocal, endPosLocal, prog);
                transform.position = position;

                yield return null;
            }

            transform.position = endPosLocal;
            SetGridPosition(endGridPos);
            StateMachine.Idle.Start();

            lerpDone();
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

            // detect running out of fuel
            if (this.IsFuelEmpty)
            {
                if (this.StateMachine.CurrentState != this.StateMachine.OutOfFuel && this.StateMachine.CurrentState != null)
                {
                    this.StateMachine.OutOfFuel.Start();
                }
            }
            else if (Game.FollowingRoot.MaxReachedGridPosition != null && Game.FollowingRoot.MaxReachedGridPosition == this.GridPosition)
            {
                // detect grabbed by root
                if (this.StateMachine.CurrentState != this.StateMachine.GrabbedByRoot && this.StateMachine.CurrentState != null)
                {
                    this.StateMachine.GrabbedByRoot.Start();
                }
            }

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

        // animation callbacks (so hacky)

        // handler for when breaking downward is completing
        public UnityAction BreakDownAnimationCompletingHandler;

        // callback when animation for breaking downward is completing
        public void BreakDownCompleting()
        {
            if (BreakDownAnimationCompletingHandler != null)
            {
                BreakDownAnimationCompletingHandler();
            }
        }

        public UnityAction BreakLeftAnimationCompletingHandler;

        public void BreakLeftCompleting()
        {
            if (BreakLeftAnimationCompletingHandler != null)
            {
                BreakLeftAnimationCompletingHandler();
            }
        }

        public UnityAction BreakRightAnimationCompletingHandler;

        public void BreakRightCompleting()
        {
            if (BreakRightAnimationCompletingHandler != null)
            {
                BreakRightAnimationCompletingHandler();
            }
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