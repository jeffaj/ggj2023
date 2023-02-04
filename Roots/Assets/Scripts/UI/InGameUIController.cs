using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InGameUIController : MonoBehaviour
{
    private TextMeshProUGUI ScoreText;
    private TextMeshProUGUI LevelText;
    private Image EnergyFillBar;

    private bool initialized = false;

    private void Init()
    {
        if (initialized)
        {
            return;
        }

        initialized = true;

        ScoreText = gameObject.transform.Find("Score").GetComponent<TextMeshProUGUI>();
        LevelText = gameObject.transform.Find("Level").GetComponent<TextMeshProUGUI>();
        EnergyFillBar = gameObject.transform.Find("EnergyBar/Fill").GetComponent<Image>();

        UpdateScore(0);
        UpdateEnergy(1);
    }

    void Awake()
    {
        Init();
    }

    public void UpdateScore(int score)
    {
        ScoreText.text = $"Score: {score}";
    }

    public void UpdateLevel(int levelIndex) {
        LevelText.text = $"Level: {levelIndex + 1}";
    }

    // 0 to 1, 1 is full
    public void UpdateEnergy(float percent)
    {
        EnergyFillBar.fillAmount = percent;
    }
}
