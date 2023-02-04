using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITester : MonoBehaviour
{
    public List<ArtifactData> Artifacts;

    public int currIdx = 0;

    public ArtifactModalController modalController;

    public EndOfLevelModalController endOfLevelModalController;

    public InGameUIController inGameUIController;

    public void OnSet()
    {
        Debug.Log("on set!");

        modalController.UpdateWithArtifactData(Artifacts[currIdx]);
        currIdx = (currIdx + 1) % Artifacts.Count;

        modalController.OnResumeLevelHandler = OnResumeHandler;

        // endOfLevelModalController.UpdatePoints(10001235);

        inGameUIController.UpdateEnergy(0.4f);
        inGameUIController.UpdateScore(1032345);
    }

    void OnResumeHandler()
    {
        Debug.Log("RESUME");
    }
}
