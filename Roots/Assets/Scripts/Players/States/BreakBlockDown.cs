using Levels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Players.States
{
    /*
    - start break down animation
    - callback to animation done
    - start fall animation
    - start lerp down
    - when done, go to Idle
    */

    [System.Serializable]
    public class BreakBlockDown : PlayerState
    {
        #region Inspector Fields

        #endregion

        private Vector2Int _startGridPos;

        public void Start()
        {
            this.ChangeStateToSelfForce();

            Player.AnimationController.SetTrigger("BreakDown");

            Player.BreakDownAnimationCompletingHandler = () =>
            {
                var breakGridPos = Player.GridPosition + Vector2Int.down;
                var tile = Game.LevelGrid.GetTile(breakGridPos);
                // should always be present, since moving down
                tile.Interact();
                Game.LevelGrid.DestroyTile(breakGridPos);

                Player.AnimationController.SetTrigger("FallDown");

                Player.LerpToIdle(breakGridPos, .1f);
            };
        }
    }
}