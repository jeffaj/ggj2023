using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    const string GAME_SCENE_NAME = "GameScene";

    public void OnClickStart()
    {
        Debug.Log("start clicked");
        SceneManager.LoadScene(GAME_SCENE_NAME);
    }

    public void OnClickExit()
    {
        Debug.Log("exit clicked");
        Application.Quit(0);
    }
}
