using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    const string GAME_SCENE_NAME = "GameScene";

    [SerializeField]
    private IntroModalController _introModalController;

    public void OnClickStart()
    {
        Debug.Log("start clicked");

        this.gameObject.SetActive(false);
        _introModalController.Present();
    }

    public void OnClickExit()
    {
        Debug.Log("exit clicked");
        Application.Quit(0);
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
