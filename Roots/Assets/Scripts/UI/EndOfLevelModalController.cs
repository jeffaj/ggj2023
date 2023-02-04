using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class EndOfLevelModalController : MonoBehaviour
{
    public UnityEvent OnNextLevelHandler;

    private TextMeshProUGUI PointsText;

    void Awake()
    {
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
        PointsText.text = $"You Scored {points} Points";
    }
}
