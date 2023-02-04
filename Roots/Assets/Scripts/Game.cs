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
    private GameSettings _gameSettings = null;

    [Header("Scene (UI)")]

    [SerializeField]
    private InGameUIController _gameUIController = null;
    [SerializeField]
    private PauseController _pauseUIController = null;
    [SerializeField]
    private EndOfLevelModalController _endOfLevelModalController = null;
    [SerializeField]
    private GameOverModalController _gameOverModalController = null;
    [SerializeField]
    private ArtifactModalController _artifactModalController = null;

    #endregion

    private bool _gamePaused;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void OnBeforeSceneLoadRuntimeMethod()
    {
        // debug:
        UDeb.RegisterAction("start game", () =>
        {
            StartGame();
        });
    }

    private static Game _instance;

    public static GameSettings GameSettings => _instance._gameSettings;
    public static Player Player => _instance._player;
    public static LevelGrid LevelGrid => _instance._levelGrid;
    public static InGameUIController GameUIController => _instance._gameUIController;

    public static bool GamePaused => _instance._gamePaused;

    /// <summary>
    /// Score tracker.  Persists when scenes change.
    /// </summary>
    public static ScoreTracker ScoreTracker { get; private set; } = new ScoreTracker();

    /// <summary>
    /// Current level index.  Persists when scenes change.
    /// </summary>
    public static int LevelIndex { get; private set; }

    public static Game Instance => _instance;

    public static void StartGame()
    {
        ScoreTracker.FullReset();
        SceneManager.LoadScene("Scenes/GameScene");
    }

    public static void OnInteractWithArtifact(ArtifactData artifactData)
    {
        // score update
        _instance.IncrementCurrentScore(artifactData.PointValue);

        // launch artifact modal
        ArtifactModalController controller = _instance._artifactModalController;
        controller.gameObject.SetActive(true);
        controller.UpdateWithArtifactData(artifactData);
        _instance.UpdatePauseState(true);
    }

    public static void OnInteractWithDirt()
    {
        _instance.IncrementCurrentScore(_instance._gameSettings.BreakDirtPointValue);
    }

    // checks if we have failed, succeeded, etc.
    public void CheckEndConditions()
    {
        // is at last row
        if (Player.LocalPosition.y == 0)
        {
            OnWinLevel();
        }
        else if (Player.IsFuelEmpty)
        {
            OnFailLevel();
        }
    }

    private void OnWinLevel()
    {
        IncrementCurrentScore(_gameSettings.WinLevelPointValue);
        UpdatePauseState(true);
        _endOfLevelModalController.UpdatePoints(ScoreTracker.CurrentScore);
        _endOfLevelModalController.gameObject.SetActive(true);
    }

    private void OnFailLevel()
    {
        UpdatePauseState(true);
        _gameOverModalController.gameObject.SetActive(true);
    }

    private void IncrementCurrentScore(int delta)
    {
        ScoreTracker.IncrementCurrentScore(delta);
        _instance._gameUIController.UpdateScore(ScoreTracker.CurrentScore);
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

        // wire in to UI controllers
        _pauseUIController.OnResumeLevelHandler = TogglePauseModal;
        _pauseUIController.OnRestartLevelHandler = RestartLevel;
        _gameOverModalController.OnRestartLevelHandler = RestartLevel;
        _endOfLevelModalController.OnNextLevelHandler = NextLevel;
        _artifactModalController.OnResumeLevelHandler = ToggleArtifactModal;
    }

    private void Start()
    {
        // make sure various UI elements are reset
        UpdatePauseState(false);
        GameUIController.UpdateLevel(LevelIndex);
        _endOfLevelModalController.gameObject.SetActive(false);
        _gameOverModalController.gameObject.SetActive(false);
        _pauseUIController.gameObject.SetActive(false);
        _artifactModalController.gameObject.SetActive(false);

        // initialize level
        LevelConfig levelConfig = GameSettings.GetLevelConfig(LevelIndex);
        LevelGrid.Initialize(levelConfig);

        // start game
        Player.ResetFuelToFull();
        Player.IdleAt(LevelGrid.PlayerStartGridPosition);

        
    }

    private void Update()
    {
        if (!_gamePaused && PlayerInput.EscPressed)
        {
            TogglePauseModal();
        }
    }

    private void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }

    private void RestartLevel()
    {
        ScoreTracker.ResetCurrentScore();

        // for now, just start game
        StartGame();
    }

    private void NextLevel() {
        if (LevelIndex + 1 < GameSettings.LevelsCount) {
            LevelIndex++;
        } else {
            // TODO: beat game?
        }

        StartGame();
    }

    #region Pausing

    private void TogglePauseModal()
    {
        UpdatePauseState(!_gamePaused);
        _pauseUIController.gameObject.SetActive(_gamePaused);
    }

    private void UpdatePauseState(bool paused)
    {
        _gamePaused = paused;
        // TODO: time scale
    }

    #endregion

    private void ToggleArtifactModal()
    {
        UpdatePauseState(false);
        _artifactModalController.gameObject.SetActive(_gamePaused);
    }
}
