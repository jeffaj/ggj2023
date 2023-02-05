using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    const string GAME_SCENE_NAME = "GameScene";

    [SerializeField]
    private IntroModalController _introModalController;

    private void Start()
    {
        gameObject.SetActive(true);
    }

    public void OnClickStart()
    {
        Debug.Log("start clicked");

        if (!IntroModalController.WasPresented)
        {
            this.gameObject.SetActive(false);
            _introModalController.Present();
        }
        else
        {
            SceneManager.LoadScene(GAME_SCENE_NAME);
        }
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
