using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using Players;

public class ArtifactModalController : MonoBehaviour
{
    private Image ArtifactImage;
    private TextMeshProUGUI Name;
    private TextMeshProUGUI FlavorText;
    private TextMeshProUGUI PointsText;

    private bool initialized;

    public UnityAction OnResumeLevelHandler;

    public void Init()
    {
        if (initialized)
        {
            return;
        }

        initialized = true;

        ArtifactImage = gameObject.transform.Find("ArtifactImage").GetComponent<Image>();
        Name = gameObject.transform.Find("ArtifactName").GetComponent<TextMeshProUGUI>();
        FlavorText = gameObject.transform.Find("ArtifactFlavorText").GetComponent<TextMeshProUGUI>();
        PointsText = gameObject.transform.Find("PointsText").GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (PlayerInput.EscPressed)
        {
            Debug.Log("EscPressed artifact");
            if (OnResumeLevelHandler != null)
            {
                OnResumeLevelHandler.Invoke();
            }
        }
    }

    public void UpdateWithArtifactData(ArtifactData artifactData)
    {
        Init();

        Debug.Log("Update Artifact Modal Data");
        ArtifactImage.sprite = artifactData.Sprite;
        Name.text = artifactData.Name;
        FlavorText.text = artifactData.FlavorText;
        PointsText.text = $"You gained {artifactData.PointValue} points!";
    }
}
