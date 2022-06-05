using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class EventsHandler : MonoBehaviour
{
    private const float ExtremeSpeedValue = 0.1f;

    [SerializeField] private CollisionHandler _collisionHandler;
    [SerializeField] private EnemySpawner _enemiesSpawner;
    [SerializeField] private BoostersSpawner _boostersSpawner;
    [SerializeField] private Player _player;
    [SerializeField] private PlayerMover _playerMover;
    [SerializeField] private GameOverPanel _gameOverPanel;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _exitButton;

    private void OnEnable()
    {
        if (_collisionHandler == null || _enemiesSpawner == null || _boostersSpawner == null || _player == null
            || _playerMover == null || _gameOverPanel == null || _restartButton == null || _exitButton == null)
            throw new System.ArgumentNullException("Отсутствует или некорректно указан обязательный параметр. Проверьте инспектор.");

        Time.timeScale = 1;
        _gameOverPanel.Deactivate();
        _enemiesSpawner.IsBorned += _collisionHandler.AddNewEnemy;
        _boostersSpawner.IsBorned += _collisionHandler.AddNewBooster;
        _collisionHandler.EnemyDead += _player.IncreaseKilledCount;
        _restartButton.onClick.AddListener(RestartGame);
        _exitButton.onClick.AddListener(ExitGame);
    }

    private void OnDisable()
    {
        _enemiesSpawner.IsBorned -= _collisionHandler.AddNewEnemy;
        _boostersSpawner.IsBorned -= _collisionHandler.AddNewBooster;
        _collisionHandler.EnemyDead -= _player.IncreaseKilledCount;
        _restartButton.onClick.RemoveListener(RestartGame);
        _exitButton.onClick.RemoveListener(ExitGame);
    }

    private void Update()
    {
        if (_player.KilledEnemiesCount == _enemiesSpawner.EnemiesCount || _playerMover.Speed <= ExtremeSpeedValue)
            OpenGameOverPanel();
    }

    private void OpenGameOverPanel()
    {
        Time.timeScale = 0;
        _gameOverPanel.Activate();
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    private void ExitGame()
    {
        Application.Quit();
    }
}
