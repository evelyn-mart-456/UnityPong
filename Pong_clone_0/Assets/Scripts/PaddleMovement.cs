using UnityEngine;

public class PaddleMovement : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        float move = Input.GetAxis("Vertical");
        Debug.Log("Move input: " + move); // see if input is detected
        transform.position += new Vector3(0f, move * speed * Time.deltaTime, 0f);
    }
}


 