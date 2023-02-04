using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITester : MonoBehaviour
{
    public List<ArtifactData> Artifacts;

    public int currIdx = 0;

    public ArtifactModalController modalController;

    public EndOfLevelModalController endOfLevelModalController;

    public void OnSet()
    {
        Debug.Log("on set!");

        // modalController.UpdateWithArtifactData(Artifacts[currIdx]);
        // currIdx = (currIdx + 1) % Artifacts.Count;

        endOfLevelModalController.UpdatePoints(10001235);
    }
}
