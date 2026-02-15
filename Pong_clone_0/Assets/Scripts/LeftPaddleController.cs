using UnityEngine;
using Unity.Netcode;

public class LeftPaddleController : PaddleController, ICollidable
{
    public NetworkVariable<float> paddleY = new NetworkVariable<float>();

    protected override Vector2 GetInput()
    {
        // Only allow the owner to provide input
        if (!IsOwner)
            return Vector2.zero;

        float vertical = Input.GetAxis("Player1Vertical");
        return new Vector2(0, vertical);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (IsOwner)
        {
            paddleY.Value = rb.position.y; // update NetworkVariable
        }
        else
        {
            // Non-owner: follow networked value
            Vector2 pos = rb.position;
            pos.y = paddleY.Value;
            rb.MovePosition(pos);
        }
    }

    public void OnHit(Collision2D collision)
    {
        Debug.Log("Left paddle was hit by: " + collision.gameObject.name);
        GetComponent<SpriteRenderer>().color = Color.red;
        Invoke(nameof(ResetColor), 0.2f);
    }

    private void ResetColor()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
    }
}



 
