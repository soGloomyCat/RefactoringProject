using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    private const float Denominator = 2f;

    [SerializeField] private float _speed;
    [SerializeField] private float _startCooldown;

    private float _elapsedTime;
    private bool _isSpeedReduced;

    public Vector3 CurrentPosition => transform.position;
    public float Speed => _speed;

    public void IncreaseSpeed(float boostValue)
    {
        _speed *= boostValue;
        _isSpeedReduced = false;
        _elapsedTime = _startCooldown;
    }

    private void OnEnable()
    {
        if (_speed <= 0 || _startCooldown <= 0)
            throw new System.ArgumentNullException("Отсутствует или некорректно указан обязательный параметр. Проверьте инспектор.");
    }

    private void Start()
    {
        _elapsedTime = _startCooldown;
        _isSpeedReduced = false;
    }

    private void Update()
    {
        _elapsedTime -= Time.deltaTime;

        if (_elapsedTime <= 0 && _isSpeedReduced == false)
            ReduceSpeed();

        if (Input.GetKey(KeyCode.W))
            transform.Translate(0, _speed * Time.deltaTime, 0);

        if (Input.GetKey(KeyCode.S))
            transform.Translate(0, -_speed * Time.deltaTime, 0);

        if (Input.GetKey(KeyCode.A))
            transform.Translate(-_speed * Time.deltaTime, 0, 0);

        if (Input.GetKey(KeyCode.D))
            transform.Translate(_speed * Time.deltaTime, 0, 0);
    }

    private void ReduceSpeed()
    {
        _speed /= Denominator;
        _isSpeedReduced = true;
    }
}
