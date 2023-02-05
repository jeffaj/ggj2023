using Levels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Players.States
{

    [System.Serializable]
    public class GrabbedByRoot : PlayerState
    {

        #region Inspector Fields

        [SerializeField]
        private float _playAnimationDuration = 1.0f;

        #endregion

        public void Start()
        {
            this.ChangeStateToSelfForce();

            Player.AnimationController.SetTrigger("DeathByRoot");
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
                Game.OnFailLevelRoot("The root has crushed you!");
                this.StateMachine.ChangeState(null);
            }
        }

        private float _stateStartTime;
    }
}