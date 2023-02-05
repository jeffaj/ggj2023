using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    [SerializeField]
    public int FuelMax = 100;

    [SerializeField]
    public int MoveCost = -2;

    [SerializeField]
    public int BreakDirtCost = -3;

    [SerializeField]
    public int BreakDirtPointValue = 5;

    [SerializeField]
    public int WinLevelPointValue = 3000;

    [SerializeField]
    public float RootSpeedBlocksPerSecond = 0.25f;

    [SerializeField]
    public float RootStartDelaySeconds = 2;

    [SerializeField]
    public bool RootEnabled = true;

    [SerializeField]
    private int _levelsCount = 9999;

    [SerializeField]
    private LevelConfig[] _levelConfigs = null;

    public int LevelsCount => Mathf.Min(_levelsCount, _levelConfigs.Length);

    public LevelConfig GetLevelConfig(int levelIndex)
    {
        levelIndex = Mathf.Clamp(levelIndex, 0, this.LevelsCount - 1);
        return _levelConfigs[levelIndex];
    }
}
