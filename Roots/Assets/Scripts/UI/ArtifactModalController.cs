using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class ArtifactModalController : MonoBehaviour
{
    public UnityEvent OnResumeLevelHandler;

    private Image ArtifactImage;
    private TextMeshProUGUI FlavorText;
    private TextMeshProUGUI PointsText;

    public void Awake()
    {
        ArtifactImage = gameObject.transform.Find("ArtifactImage").GetComponent<Image>();
        FlavorText = gameObject.transform.Find("ArtifactFlavorText").GetComponent<TextMeshProUGUI>();
        PointsText = gameObject.transform.Find("PointsText").GetComponent<TextMeshProUGUI>();
    }

    public void OnResumeClicked()
    {
        Debug.Log("OnResumeClicked");
        if (OnResumeLevelHandler != null)
        {
            OnResumeLevelHandler.Invoke();
        }
    }

    public void UpdateWithArtifactData(ArtifactData artifactData)
    {
        Debug.Log("Update Artifact Modal Data");
        ArtifactImage.sprite = artifactData.Sprite;
        FlavorText.text = artifactData.FlavorText;
        PointsText.text = $"You gained {artifactData.PointValue} points!";
    }
}
