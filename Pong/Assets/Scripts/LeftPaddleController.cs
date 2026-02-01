using UnityEngine;

public class LeftPaddleController : PaddleController
{
    protected override Vector2 GetInput()
    {
        // Horizontal axis with reversed alt buttons
        float horizontal = Input.GetAxis("Player1Horizontal");

        // Vertical axis (example: W/S keys)
        float vertical = Input.GetAxis("Player1Vertcial");

        return new Vector2(horizontal, vertical);
    }
}


