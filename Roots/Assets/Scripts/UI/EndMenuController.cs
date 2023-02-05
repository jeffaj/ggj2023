using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenuController : MonoBehaviour {
    
    private const string _mainMenuSceneName = "MainMenu";

    public void ReturnToMainMenu() {
        SceneManager.LoadScene(_mainMenuSceneName);
    }
}
