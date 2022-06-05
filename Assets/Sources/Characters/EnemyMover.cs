using System.Collections;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    private const float CircleRadius = 4;

    [SerializeField] private float _speed;

    private Vector3 _targetPosition;
    private Coroutine _moveCoroutine;

    private void OnEnable()
    {
        if (_speed <= 0)
            throw new System.ArgumentNullException("Отсутствует или некорректно указан обязательный параметр. Проверьте инспектор.");
    }

    private void Start()
    {
        _targetPosition = Random.insideUnitCircle * CircleRadius;

        if (_moveCoroutine != null)
            StopCoroutine(_moveCoroutine);

        _moveCoroutine = StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        while (transform.position != _targetPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _speed * Time.deltaTime);
            yield return null;

            if (transform.position == _targetPosition)
                OverrideTarget();
        }
    }

    private void OverrideTarget()
    {
        _targetPosition = Random.insideUnitCircle * CircleRadius;
    }
}
