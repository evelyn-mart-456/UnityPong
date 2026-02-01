

using UnityEngine;

public class RightPaddleController : PaddleController
{
    protected override Vector2 GetInput()
    {
        // Horizontal axis with reversed alt buttons
        float horizontal = Input.GetAxis("Player2Horizontal");

        // Vertical axis (example: Up/Down arrows)
        float vertical = Input.GetAxis("Player2Vertical");

        return new Vector2(horizontal, vertical);
    }
}
