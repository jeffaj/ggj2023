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

    private float targetEnergy;

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

        targetEnergy = 1;
        EnergyFillBar.fillAmount = 1;
    }

    void Awake()
    {
        Init();
    }

    void Update()
    {
        float speedupCutoff = 0.1f;
        float percentUpdatePerSecond = 0.001f;
        float diff = Mathf.Abs(EnergyFillBar.fillAmount - targetEnergy);
        if (diff < 0.001)
        {
            EnergyFillBar.fillAmount = targetEnergy;
            return;
        }

        float updateSpeed = Mathf.Lerp(0, percentUpdatePerSecond, Mathf.Clamp(diff, 0, speedupCutoff));

        // Debug.Log($"update speed: {updateSpeed}, delta: {diff}");

        EnergyFillBar.fillAmount = Mathf.MoveTowards(EnergyFillBar.fillAmount, targetEnergy, updateSpeed * Time.time);
    }

    public void UpdateScore(int score)
    {
        ScoreText.text = $"Score: {score}";
    }

    public void UpdateLevel(int levelIndex)
    {
        LevelText.text = $"Level: {levelIndex + 1}";
    }

    // 0 to 1, 1 is full
    public void UpdateEnergy(float percent)
    {
        targetEnergy = percent;
    }
}
