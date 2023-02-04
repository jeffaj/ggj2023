using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameOverModalController : MonoBehaviour
{
    public UnityAction OnRestartLevelHandler;

    public void OnRestartLevelClicked()
    {
        Debug.Log("OnRestartLevelClicked game over");
        if (OnRestartLevelHandler != null)
        {
            OnRestartLevelHandler.Invoke();
        }
    }
}
