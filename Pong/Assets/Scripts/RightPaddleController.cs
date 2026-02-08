

using UnityEngine;

public class RightPaddleController : PaddleController, ICollidable
{
    protected override Vector2 GetInput()
    {
        float horizontal = Input.GetAxis("Player2Horizontal");
        float vertical = Input.GetAxis("Player2Vertical");

        return new Vector2(horizontal, vertical);
    }

    public void OnHit(Collision2D collision)
    {
        Debug.Log("Right paddle was hit by: " + collision.gameObject.name);

        // Optional: visual feedback
        GetComponent<SpriteRenderer>().color = Color.red;
        Invoke(nameof(ResetColor), 0.2f);
    }

    private void ResetColor()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
    }
}

