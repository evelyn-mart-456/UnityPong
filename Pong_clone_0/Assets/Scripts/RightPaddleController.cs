using UnityEngine;
using Unity.Netcode;

public class RightPaddleController : PaddleController, ICollidable
{
    public NetworkVariable<float> paddleY = new NetworkVariable<float>();

    protected override Vector2 GetInput()
    {
        if (!IsOwner)
            return Vector2.zero;

        float vertical = Input.GetAxis("Player2Vertical");
        return new Vector2(0, vertical);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (IsOwner)
        {
            paddleY.Value = rb.position.y;
        }
        else
        {
            Vector2 pos = rb.position;
            pos.y = paddleY.Value;
            rb.MovePosition(pos);
        }
    }

    public void OnHit(Collision2D collision)
    {
        Debug.Log("Right paddle was hit by: " + collision.gameObject.name);
        GetComponent<SpriteRenderer>().color = Color.red;
        Invoke(nameof(ResetColor), 0.2f);
    }

    private void ResetColor()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
    }
}



