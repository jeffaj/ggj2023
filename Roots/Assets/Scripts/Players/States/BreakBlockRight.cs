using Levels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Players.States
{
    /*
    - start break right animation
    - callback to animation done
    - start walk animation
    - start lerp
    - when done, go to Idle
    */

    [System.Serializable]
    public class BreakBlockRight : PlayerState
    {
        #region Inspector Fields

        #endregion

        private Vector2Int _startGridPos;

        public void Start()
        {
            this.ChangeStateToSelfForce();

            Player.AnimationController.SetTrigger("BreakRight");

            Player.BreakRightAnimationCompletingHandler = () =>
            {
                var breakGridPos = Player.GridPosition + Vector2Int.right;

                var tile = Game.LevelGrid.GetTile(breakGridPos);
                tile.Interact();
                Game.LevelGrid.DestroyTile(breakGridPos);

                Player.LerpToIdle(breakGridPos);
            };
        }
    }
}