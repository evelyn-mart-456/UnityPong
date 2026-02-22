using UnityEngine;
using Unity.Netcode;

public class AutoStartHost : MonoBehaviour
{
    void Start()
    {
        NetworkManager.Singleton.StartHost();
        Debug.Log("Host started");
    }
}
