using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Roots/CreateArtifact")]
public class ArtifactData : ScriptableObject
{
    public Sprite Sprite;
    public string FlavorText;
    public int PointValue;
}
