using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    [SerializeField]
    public int FuelMax = 100;

    [SerializeField]
    public int MoveCost = 5;

    [SerializeField]
    public int BreakDirtPointValue = 5;

    [SerializeField]
    public int WinLevelPointValue = 3000;

    [SerializeField]
    private LevelConfig[] _levelConfigs = null;

    public LevelConfig GetLevelConfig(int levelIndex) {
        levelIndex = Mathf.Clamp(levelIndex, 0, _levelConfigs.Length - 1);
        return _levelConfigs[levelIndex];
    }
}
