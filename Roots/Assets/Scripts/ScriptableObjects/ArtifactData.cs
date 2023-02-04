using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Roots/CreateArtifact")]
public class ArtifactData : ScriptableObject
{
    public Sprite Sprite;
    public string Name;
    public string FlavorText;
    public int PointValue;
}
