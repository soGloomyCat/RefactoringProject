using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollisionHandler : MonoBehaviour
{
    private const float TargetDistance = 0.5f;

    [SerializeField] private PlayerMover _player;

    private List<Enemy> _enemies;
    private List<SpeedBooster> _boosters;

    public event UnityAction EnemyDead;

    public void AddNewEnemy(Enemy enemy)
    {
        _enemies.Add(enemy);
    }

    public void AddNewBooster(SpeedBooster booster)
    {
        _boosters.Add(booster);
    }

    private void OnEnable()
    {
        if (_player == null)
            throw new System.ArgumentNullException("Отсутствует или некорректно указан обязательный параметр. Проверьте инспектор.");

        _enemies = new List<Enemy>();
        _boosters = new List<SpeedBooster>();
    }

    private void DetermineEnemyCollision()
    {
        foreach (Enemy enemy in _enemies)
        {
            if (Vector3.Distance(_player.CurrentPosition, enemy.CurrentPosition) < TargetDistance)
            {
                Remove(enemy);
                break;
            }
        }
    }

    private void DetermineBoosterCollision()
    {
        foreach (SpeedBooster booster in _boosters)
        {
            if (Vector3.Distance(_player.CurrentPosition, booster.CurrentPosition) < TargetDistance)
            {
                _player.IncreaseSpeed(booster.BoostValue);
                Remove(booster);
                break;
            }
        }
    }

    private void Update()
    {
        DetermineEnemyCollision();
        DetermineBoosterCollision();
    }

    private void Remove(Enemy enemy)
    {
        enemy.Deactivate();
        _enemies.Remove(enemy);
        EnemyDead?.Invoke();
    }

    private void Remove(SpeedBooster booster)
    {
        booster.Deactivate();
        _boosters.Remove(booster);
    }
}
