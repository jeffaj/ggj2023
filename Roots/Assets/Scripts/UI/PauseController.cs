using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PauseController : MonoBehaviour
{
    public UnityEvent OnResumeLevelHandler;
    public UnityEvent OnRestartLevelHandler;

    public void OnResumeClicked()
    {
        Debug.Log("OnResumeClicked");
        if (OnResumeLevelHandler != null)
        {
            OnResumeLevelHandler.Invoke();
        }
    }

    public void OnRestartClicked()
    {
        Debug.Log("OnRestartClicked");
        if (OnRestartLevelHandler != null)
        {
            OnRestartLevelHandler.Invoke();
        }
    }

    public void OnTutorialClicked()
    {
        Debug.Log("OnTutorialClicked");
    }

    public void OnExitClicked()
    {
        Debug.Log("exit clicked");
        Application.Quit(0);
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
