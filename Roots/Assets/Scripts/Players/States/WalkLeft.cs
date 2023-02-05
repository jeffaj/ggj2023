using Levels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Players.States
{
    [System.Serializable]
    public class WalkLeft : PlayerState
    {
        #region Inspector Fields

        #endregion

        private Vector2Int _startGridPos;

        public void Start()
        {
            this.ChangeStateToSelfForce();
            Player.StartCoroutine(MovePlayer(this.Player.GridPosition));
        }

        IEnumerator MovePlayer(Vector2Int startPos)
        {
            Vector2Int endGridPos = startPos + new Vector2Int(-1, 0);

            Debug.Log($"vec2int: {startPos} -> {endGridPos}");

            Vector3 startPosLocal = Game.LevelGrid.GetLocalPosition(startPos);
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