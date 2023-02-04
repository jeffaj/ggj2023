using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class EndOfLevelModalController : MonoBehaviour
{
    public UnityAction OnNextLevelHandler;

    private TextMeshProUGUI PointsText;

    private bool initialized;

    public void Init()
    {
        if (initialized)
        {
            return;
        }

        initialized = true;

        PointsText = gameObject.transform.Find("PointsText").GetComponent<TextMeshProUGUI>();
    }

    public void OnNextLevelClicked()
    {
        Debug.Log("OnNextLevelClicked");
        if (OnNextLevelHandler != null)
        {
            OnNextLevelHandler.Invoke();
        }
    }

    public void UpdatePoints(int points)
    {
        Init();

        PointsText.text = $"You Scored {points} Points";
    }
}
