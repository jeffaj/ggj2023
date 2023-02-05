using Levels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Players.States
{
    [System.Serializable]
    public class WalkRight : PlayerState
    {
        #region Inspector Fields

        #endregion

        private Vector2Int _startGridPos;

        public void Start()
        {
            this.ChangeStateToSelfForce();

            Player.AnimationController.SetTrigger("WalkRightToIdle");

            Player.LerpToIdle(Player.GridPosition + Vector2Int.right, .2f);
        }
    }
}