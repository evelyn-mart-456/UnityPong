using UnityEngine;

using UnityEngine;

public class BallMovement : NetworkedObject, ICollidable
{
    private float speed = 5f;
    private Vector2 direction = Vector2.right;

    private Rigidbody2D rb;

    // Encapsulation stays (this is good)
    public float Speed
    {
        get { return speed; }
        set
        {
            if (value >= 0)
                speed = value;
        }
    }

    public Vector2 Direction
    {
        get { return direction; }
        set
        {
            if (value != Vector2.zero)
                direction = value.normalized;
        }
    }

    protected void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
        }

        rb.gravityScale = 0;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
    }

    public override void Initialize()
    {
        // Placeholder for future networking
        Debug.Log("Ball initialized");
    }

    public override int GetNetworkId()
    {
        return 0; // placeholder ID
    }

    void FixedUpdate()
    {
        rb.velocity = direction * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ICollidable collidable = collision.gameObject.GetComponent<ICollidable>();

        if (collidable != null)
        {
            collidable.OnHit(collision);
        }
    }

    public void OnHit(Collision2D collision)
    {
        // Simple bounce logic
        Vector2 normal = collision.contacts[0].normal;
        direction = Vector2.Reflect(direction, normal).normalized;
    }
}
