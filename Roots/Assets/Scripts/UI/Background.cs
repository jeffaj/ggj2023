using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class Background : MonoBehaviour {

        #region Inspector Fields

        [SerializeField]
        private int _imageInterval = 3;

        [SerializeField]
        private Sprite[] _bgImages = null;

        #endregion

        public void UpdateBackground(int levelIndex) {
            int imageIndex = levelIndex / _imageInterval;
            imageIndex %= _bgImages.Length;

            _image.sprite = _bgImages[imageIndex];
        }

        private void Awake() {
            _image = this.GetComponent<Image>();
        }

        private Image _image;

    }
}