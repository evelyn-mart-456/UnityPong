using UnityEngine;
using Unity.Netcode;

public class BallController : NetworkBehaviour, ICollidable
{
    public float speed = 8f;
    private Rigidbody2D rb;

    public override void OnNetworkSpawn()
    {
        rb = GetComponent<Rigidbody2D>();
        
        // Only the server controls the ball
        if (IsServer)
        {
            LaunchBall();
        }
    }

    private void LaunchBall()
    {
        float xDir = Random.Range(0, 2) == 0 ? -1 : 1;
        float yDir = Random.Range(-0.5f, 0.5f);
        rb.velocity = new Vector2(xDir, yDir).normalized * speed;
    }

    public void OnHit(Collision2D collision)
    {
        // Optional: ball hit feedback
        Debug.Log("Ball hit: " + collision.gameObject.name);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (TryGetComponent<ICollidable>(out var collidable))
        {
            collidable.OnHit(collision);
        }
    }
}

