using Levels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Players.States
{

    [System.Serializable]
    public class Idle : PlayerState
    {

        #region Inspector Fields

        #endregion

        public void Start()
        {
            this.ChangeStateToSelfForce();
        }

        protected override void Update()
        {

            bool leftPressed = PlayerInput.LeftPressed;
            bool rightPressed = PlayerInput.RightPressed;
            bool downPressed = PlayerInput.DownPressed;

            PlayerCommand command = PlayerCommand.None;

            if (leftPressed)
            {
                command = PlayerCommand.Left;
            }
            else if (rightPressed)
            {
                command = PlayerCommand.Right;
            }
            else if (downPressed)
            {
                command = PlayerCommand.Down;
            }

            if (command != PlayerCommand.None)
            {
                this.ExecuteCommand(command);
            }
        }

        private void ExecuteCommand(PlayerCommand command)
        {
            Vector2Int gridPos = this.Player.GridPosition;
            Vector2Int newPos = gridPos;
            switch (command)
            {
                case PlayerCommand.Left:
                    newPos.x--;
                    break;
                case PlayerCommand.Right:
                    newPos.x++;
                    break;
                case PlayerCommand.Down:
                    newPos.y--;
                    break;
            }

            // can't move if would go out of bounds
            if (!Game.LevelGrid.IsValidPosition(newPos))
                return;

            // can't move if new tile can't be passed through
            Tile tile = Game.LevelGrid.GetTile(newPos);
            if (tile != null && !tile.IsPassable)
                return;

            if (tile == null)
            {
                switch (command)
                {
                    case PlayerCommand.Left:
                        Player.StateMachine.WalkLeft.Start();
                        break;
                    case PlayerCommand.Right:
                        Player.StateMachine.WalkRight.Start();
                        break;
                }
            }
            else if (!tile.IsPassable)
            {
                // TODO
            }
            else
            { // tile passable
                switch (command)
                {
                    case PlayerCommand.Left:
                        Player.StateMachine.BreakBlockLeft.Start();
                        break;
                    case PlayerCommand.Right:
                        Player.StateMachine.BreakBlockRight.Start();
                        break;
                    case PlayerCommand.Down:
                        Player.StateMachine.BreakBlockDown.Start();
                        break;
                }
            }
        }
    }
}