using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Vector3 CurrentPosition => transform.position;

    public void Deactivate()
    {
        Destroy(gameObject);
    }
}
