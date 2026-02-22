using UnityEngine;
using Unity.Netcode;

public class BallController : NetworkBehaviour
{
    public float speed = 5.0f;

    private Rigidbody2D rb;
    private Vector2 direction;

  void Start()
{
    rb = GetComponent<Rigidbody2D>();

    if (rb == null)
    {
        Debug.LogError("Ball missing Rigidbody2D!");
        return;
    }

    direction = new Vector2(1f, 1f).normalized;
}

 
  
        void FixedUpdate()
{
    if (!IsServer) return;

    if (rb == null) return;

    rb.velocity = direction * speed;
}
    

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!IsServer) return;

        ICollidable collidable = collision.gameObject.GetComponent<ICollidable>();
        if (collidable != null)
        {
            collidable.OnHit(collision);
        }

        OnHit(collision);
    }

    public void OnHit(Collision2D collision)
    {
        if (collision.gameObject.name == "top" || collision.gameObject.name == "bottom")
        {
            direction = new Vector2(direction.x, -direction.y);
        }
        else if (collision.gameObject.name == "LeftPaddleController" ||
                 collision.gameObject.name == "RightPaddleController")
        {
            direction = new Vector2(-direction.x, direction.y);
        }

        direction = direction.normalized;
    }
}
   