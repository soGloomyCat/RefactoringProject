using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    private const float CircleRadius = 5f;

    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private int _enemiesCount;
    [SerializeField] private float _cooldownBeforeNextSpawn;

    private float _elapsedTime;
    private int _currentSpawnedEnemies;

    public int EnemiesCount => _enemiesCount;

    public event UnityAction<Enemy> IsBorned;

    private void OnEnable()
    {
        if (_enemyPrefab == null || _enemiesCount <= 0 || _cooldownBeforeNextSpawn <= 0)
            throw new System.ArgumentNullException("Отсутствует или некорректно указан обязательный параметр. Проверьте инспектор.");
    }

    private void Start()
    {
        _currentSpawnedEnemies = 0;
        Spawn();
    }

    private void Update()
    {
        if (_currentSpawnedEnemies < _enemiesCount && _elapsedTime <= 0)
            Spawn();

        _elapsedTime -= Time.deltaTime;
    }

    private void Spawn()
    {
        Enemy tempEnemy;

        tempEnemy = Instantiate(_enemyPrefab, transform);
        tempEnemy.transform.position = Random.insideUnitCircle * CircleRadius;
        IsBorned?.Invoke(tempEnemy);
        _elapsedTime = _cooldownBeforeNextSpawn;
        _currentSpawnedEnemies++;
    }
}
