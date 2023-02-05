using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Players;
using UnityEngine.Audio;

// pause modal controller
public class PauseController : MonoBehaviour
{
    [SerializeField]
    private AudioMixerSnapshot _unpausedSnapshot = null;
    [SerializeField]
    private AudioMixerSnapshot _pausedSnapshot = null;

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

    private void OnEnable() {
        _pausedSnapshot.TransitionTo(0.2f);
    }
    private void OnDisable() {
        _unpausedSnapshot.TransitionTo(0.2f);
    }
}
