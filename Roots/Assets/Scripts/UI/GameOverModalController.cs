using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class GameOverModalController : MonoBehaviour
{
    public UnityAction OnExitToMainHandler;

    private TextMeshProUGUI _gameOverText;

    private bool initialized;

    private void Init()
    {
        if (initialized)
        {
            return;
        }

        initialized = true;

        _gameOverText = transform.Find("FailText").GetComponent<TextMeshProUGUI>();
    }

    public void OnExitToMainClicked()
    {
        Debug.Log("OnRestartLevelClicked game over");
        if (OnExitToMainHandler != null)
        {
            OnExitToMainHandler.Invoke();
        }
    }

    public void UpdateGameOverText(string text)
    {
        Init();

        _gameOverText.text = text;
    }
}
