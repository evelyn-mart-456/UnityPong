using UnityEngine;

public abstract class PaddleController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 10f;
    public float yLimit = 4.5f;

    protected Rigidbody2D rb;
    protected Vector2 velocity;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
            rb.gravityScale = 0;
        }
    }

    protected virtual void FixedUpdate()
    {
        Vector2 input = GetInput();

        velocity = input * speed;

        Vector2 newPosition = rb.position + velocity * Time.fixedDeltaTime;
        newPosition.y = Mathf.Clamp(newPosition.y, -yLimit, yLimit);

        rb.MovePosition(newPosition);
    }

    // 🔴 ABSTRACT: no body allowed
    protected abstract Vector2 GetInput();
}

