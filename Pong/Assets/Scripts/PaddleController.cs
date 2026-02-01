using UnityEngine;

public class PaddleController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 10f;  // Paddle movement speed
    public float yLimit = 4.5f; // Vertical clamp (depends on your camera size)

    protected Rigidbody2D rb;
    protected Vector2 velocity;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
            rb.gravityScale = 0; // Paddles should not fall
        }
    }

    protected virtual void FixedUpdate()
    {
        Vector2 input = GetInput();

        // Calculate velocity
        velocity = input * speed;

        // Move the paddle
        Vector2 newPosition = rb.position + velocity * Time.fixedDeltaTime;

        // Clamp vertical position
        newPosition.y = Mathf.Clamp(newPosition.y, -yLimit, yLimit);

        rb.MovePosition(newPosition);
    }

    /// <summary>
    /// Virtual method to get input. Each player overrides this.
    /// </summary>
    /// <returns>Vector2 direction</returns>
    protected virtual Vector2 GetInput()
    {
        return Vector2.zero; // Base class does nothing
    }
}
