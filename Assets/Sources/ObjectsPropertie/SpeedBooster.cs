using UnityEngine;

public class SpeedBooster : MonoBehaviour
{
    [SerializeField] private float _boostValue;

    public Vector3 CurrentPosition => transform.position;
    public float BoostValue => _boostValue;

    public void Deactivate()
    {
        Destroy(gameObject);
    }

    private void OnEnable()
    {
        if (_boostValue <= 0)
            throw new System.ArgumentNullException("Отсутствует или некорректно указан обязательный параметр. Проверьте инспектор.");
    }
}
