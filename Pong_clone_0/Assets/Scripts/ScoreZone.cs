using UnityEngine;
using Unity.Netcode;

public class ScoreZone : MonoBehaviour
{
    public bool isLeftZone;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Ball")) return;

        if (!NetworkManager.Singleton.IsServer) return;

        if (GameManager.Instance == null) return;

        if (isLeftZone)
            GameManager.Instance.AddRightScore();
        else
            GameManager.Instance.AddLeftScore();
    }
}
