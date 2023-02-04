using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactSetTest : MonoBehaviour
{
    public List<ArtifactData> Artifacts;

    public int currIdx = 0;

    public ArtifactModalController modalController;

    public void OnSet()
    {
        Debug.Log("on set!");

        modalController.UpdateWithArtifactData(Artifacts[currIdx]);
        currIdx = (currIdx + 1) % Artifacts.Count;
    }
}
