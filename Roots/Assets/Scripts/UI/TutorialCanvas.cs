using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI {
    public class TutorialCanvas : MonoBehaviour {

        public void PlayPressed() {
            SceneManager.LoadScene("GameScene");
        }

        public void BackPressed() {
            SceneManager.LoadScene("MainMenu");
        }

    }
}