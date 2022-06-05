using UnityEngine;

public class GameOverPanel : MonoBehaviour
{
    public void Activate() => gameObject.SetActive(true);

    public void Deactivate() => gameObject.SetActive(false);
}
