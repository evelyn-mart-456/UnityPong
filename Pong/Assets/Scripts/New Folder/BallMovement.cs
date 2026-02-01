using UnityEngine;

public class BallMovement : MonoBehaviour
{
    // Private fields for speed and direction
    private float speed = 5f;
    private Vector2 direction = Vector2.right;

    // Public properties (getter/setter) for encapsulation
    public float Speed
    {
        get { return speed; }
        set
        {
            if (value >= 0) // optional validation
                speed = value;
        }
    }

    public Vector2 Direction
    {
        get { return direction; }
        set
        {
            if (value != Vector2.zero) // avoid zero direction
                direction = value.normalized;
        }
    }

    // Use FixedUpdate for consistent physics movement
    void FixedUpdate()
    {
        // Move the ball
        transform.position += (Vector3)(direction * speed * Time.fixedDeltaTime);
    }
}
