using Levels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Players.States
{
    /*
    - start break left animation
    - callback to animation done
    - start walk animation
    - start lerp
    - when done, go to Idle
    */

    [System.Serializable]
    public class BreakBlockDownFail : PlayerState
    {
        #region Inspector Fields

        #endregion

        private Vector2Int _startGridPos;

        public void Start()
        {
            this.ChangeStateToSelfForce();

            Player.AnimationController.SetTrigger("BreakDown");

            AudioManager.Instance.PlayRockDrill();

            AudioManager.Instance.PlayRobotMove();

            Player.BreakDownAnimationCompletingHandler = () =>
            {
                Player.StateMachine.Idle.Start();
            };
        }
    }
}