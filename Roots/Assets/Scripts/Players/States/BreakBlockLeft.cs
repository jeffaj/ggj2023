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
    public class BreakBlockLeft : PlayerState
    {
        #region Inspector Fields

        #endregion

        private Vector2Int _startGridPos;

        public void Start()
        {
            this.ChangeStateToSelfForce();

            Player.AnimationController.SetTrigger("BreakLeft");
            AudioManager.Instance.PlayDrill();

            Player.BreakLeftAnimationCompletingHandler = () =>
            {
                var breakGridPos = Player.GridPosition + Vector2Int.left;

                var tile = Game.LevelGrid.GetTile(breakGridPos);
                tile.Interact();
                Game.LevelGrid.HideTile(breakGridPos);

                AudioManager.Instance.PlayRobotMove();

                Player.LerpToIdle(breakGridPos, .1f, () =>
                {
                    tile.MovedIntoAfterDestroyed();
                    Game.LevelGrid.DestroyTile(breakGridPos);
                });
            };
        }
    }
}