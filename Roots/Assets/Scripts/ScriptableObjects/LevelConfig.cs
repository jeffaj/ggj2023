using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Roots/LevelConfig")]
public class LevelConfig : ScriptableObject {

    [SerializeField]
    [Range(0f, 0.5f)]
    private float _stoneDistribution = 0.1f;
    [SerializeField]
    [Range(0f, 0.5f)]
    private float _fuelDistribution = 0.1f;
    [SerializeField]
    [Range(0, 100)]
    private int _fuelPerBlock = 10;

    public float StoneDistribution => _stoneDistribution;
    public float FuelDistribution => _fuelDistribution;
    public int FuelPerBlock => _fuelPerBlock;

}
