using UnityEngine;
using UnityEngine.Events;

public class BoostersSpawner : MonoBehaviour
{
    private const float CircleRadius = 5f;

    [SerializeField] private SpeedBooster _boosterPrefab;
    [SerializeField] private float _cooldownBeforeNextSpawn;

    private float _elapsedTime;

    public event UnityAction<SpeedBooster> IsBorned;

    private void OnEnable()
    {
        if (_boosterPrefab == null || _cooldownBeforeNextSpawn <= 0)
            throw new System.ArgumentNullException("Отсутствует или некорректно указан обязательный параметр. Проверьте инспектор.");
    }

    private void Start()
    {
        Spawn();
    }

    private void Update()
    {
        if (_elapsedTime <= 0)
            Spawn();

        _elapsedTime -= Time.deltaTime;
    }

    private void Spawn()
    {
        SpeedBooster tempBooster;

        tempBooster = Instantiate(_boosterPrefab, transform);
        tempBooster.transform.position = Random.insideUnitCircle * CircleRadius;
        IsBorned?.Invoke(tempBooster);
        _elapsedTime = _cooldownBeforeNextSpawn;
    }
}
