using Levels;
using Players;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{

    #region Inspector Fields

    [Header("Scene")]

    [SerializeField]
    private Player _player = null;
    [SerializeField]
    private LevelGrid _levelGrid = null;
    [SerializeField]
    private InGameUIController _gameUIController = null;

    #endregion

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void OnBeforeSceneLoadRuntimeMethod()
    {
        // debug:
        UDeb.RegisterAction("start game", () =>
        {
            StartGame();
        });
    }

    public static Player Player => _instance._player;
    public static LevelGrid LevelGrid => _instance._levelGrid;
    public static InGameUIController GameUIController => _instance._gameUIController;

    public static void StartGame()
    {
        SceneManager.LoadScene("Scenes/GameScene");
    }

    private void Awake()
    {
        if (_instance != null)
        {
            Debug.LogError($"There can only be one {nameof(Game)}");
            Destroy(this.gameObject);
            return;
        }
        _instance = this;
    }

    private void Start()
    {
        // start game
        Player.ResetFuelToFull();
        Player.IdleAt(LevelGrid.PlayerStartGridPosition);
    }

    private void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }

    private static Game _instance;
}
