using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Levels
{
    public abstract class Tile : MonoBehaviour
    {

        #region Inspector Fields

        [SerializeField]
        private Transform _childCubeTransform = null;

        #endregion

        public abstract bool IsPassable { get; }

        public virtual void Interact() { }

        public virtual void MovedIntoAfterDestroyed() { }

        protected void Awake()
        {
            this.RandomDiceRotate(_childCubeTransform);
        }
        protected void RandomDiceRotate(Transform transform)
        {
            Vector3 rotateAmount = new Vector3();
            rotateAmount.x = Random.value < 0.5 ? 0 : 90;
            rotateAmount.y = Random.value < 0.5 ? 0 : 90;
            rotateAmount.z = Random.value < 0.5 ? 0 : 90;
            transform.Rotate(rotateAmount);
        }
    }
}