using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Game {

    public static void StartGame() {
        SceneManager.LoadScene("Scenes/GameScene");
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void OnBeforeSceneLoadRuntimeMethod() {
        // debug:
        UDeb.RegisterAction("start game", () => {
            StartGame();
        });
    }

}
