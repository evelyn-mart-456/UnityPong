using UnityEngine;
using Unity.Netcode;

public class PaddleController : NetworkBehaviour, ICollidable
{
    protected float speed = 8f;
    protected Rigidbody2D rb;

    private NetworkVariable<float> networkPositionY =
        new NetworkVariable<float>(
            0f,
            NetworkVariableReadPermission.Everyone,
            NetworkVariableWritePermission.Owner
        );

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (rb != null)
            rb.bodyType = RigidbodyType2D.Kinematic;
        else
            Debug.LogError(gameObject.name + " is missing Rigidbody2D!");
    }

    public override void OnNetworkSpawn()
{
    if (OwnerClientId == 0)
        transform.position = new Vector3(-8f, 0f, 0f);
    else
        transform.position = new Vector3(8f, 0f, 0f);

    networkPositionY.Value = transform.position.y;
}

    void FixedUpdate()
    {
        if (rb == null) return;

        if (IsOwner)
        {
            float inputValue = Input.GetAxis(GetInputAxisName());
            rb.velocity = new Vector2(0f, inputValue * speed);

            networkPositionY.Value = transform.position.y;
        }
        else
        {
            Vector3 pos = transform.position;
            pos.y = networkPositionY.Value;
            transform.position = pos;
        }
    }

    protected virtual string GetInputAxisName()
    {
        return "Vertical";
    }

    public void OnHit(Collision2D collision)
    {
        Debug.Log(gameObject.name + " was hit by ball");
    }
}
