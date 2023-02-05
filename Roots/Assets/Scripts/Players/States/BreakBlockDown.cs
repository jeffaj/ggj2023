using Levels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Players.States
{
    [System.Serializable]
    public class BreakBlockDown : PlayerState
    {
        #region Inspector Fields

        #endregion

        private Vector2Int _startGridPos;

        public void Start()
        {
            this.ChangeStateToSelfForce();

            var breakGridPos = Player.GridPosition + Vector2Int.down;
            var tile = Game.LevelGrid.GetTile(breakGridPos);
            // should always be present, since moving down
            tile.Interact();
            Game.LevelGrid.DestroyTile(breakGridPos);

            Player.StartCoroutine(MovePlayer());
        }

        IEnumerator MovePlayer()
        {
            Vector2Int startGridPos = Player.GridPosition;
            Vector2Int endGridPos = startGridPos + Vector2Int.down;

            Debug.Log($"vec2int: {startGridPos} -> {endGridPos}");

            Vector3 startPosLocal = Game.LevelGrid.GetLocalPosition(startGridPos);
            Vector3 endPosLocal = Game.LevelGrid.GetLocalPosition(endGridPos);

            Debug.Log($"vec3: {startPosLocal} -> {endPosLocal}");

            float startTime = Time.time;
            float duration = 0.1f; // TODO: setting or const

            while (startTime + duration > Time.time)
            {
                float prog = (Time.time - startTime) / duration;

                var position = Vector3.Lerp(startPosLocal, endPosLocal, prog);
                Player.transform.position = position;

                yield return null;
            }

            var finalPosition = Vector3.Lerp(startPosLocal, endPosLocal, 1);
            Player.transform.position = finalPosition;
            Player.SetGridPosition(endGridPos);
            Player.StateMachine.Idle.Start();
        }
    }
}