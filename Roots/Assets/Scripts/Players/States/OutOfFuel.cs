using Levels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Players.States
{

    [System.Serializable]
    public class OutOfFuel : PlayerState
    {

        #region Inspector Fields

        [SerializeField]
        private float _playAnimationDuration = 1.0f;

        #endregion

        public void Start()
        {
            this.ChangeStateToSelfForce();

            Player.AnimationController.SetTrigger("OutOfFuel");
        }

        protected override void OnBegin()
        {
            _stateStartTime = Time.time;
        }

        protected override void Update()
        {
            float time = Time.time - _stateStartTime;

            if (time >= _playAnimationDuration)
            {
                Game.OnFailLevelFuel("You ran out of solar cells!");
                this.StateMachine.ChangeState(null);
            }
        }

        private float _stateStartTime;
    }
}