using UnityEngine;

public class Player : MonoBehaviour
{
    private int _killedEnemiesCount;

    public int KilledEnemiesCount => _killedEnemiesCount;

    public void IncreaseKilledCount() => _killedEnemiesCount++;

    private void Start() => _killedEnemiesCount = 0;
}
