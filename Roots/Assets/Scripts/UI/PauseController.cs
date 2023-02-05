using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Players;

// pause modal controller
public class PauseController : MonoBehaviour
{
    public UnityAction OnResumeLevelHandler;
    public UnityAction OnExitHandler;

    public void OnResumeClicked()
    {
        Debug.Log("OnResumeClicked");
        if (OnResumeLevelHandler != null)
        {
            OnResumeLevelHandler.Invoke();
        }
    }

    public void OnTutorialClicked()
    {
        Debug.Log("OnTutorialClicked");
    }

    public void OnExitClicked()
    {
        Debug.Log("exit clicked");
        if (OnExitHandler != null)
        {
            OnExitHandler();
        }
    }

    private void Update()
    {
        if (PlayerInput.EscPressed)
        {
            Debug.Log("EscPressed pause");
            if (OnResumeLevelHandler != null)
            {
                OnResumeLevelHandler.Invoke();
            }
        }
    }
}
