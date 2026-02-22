using UnityEngine;
using Unity.Netcode;
using TMPro;

public class GameManager : NetworkBehaviour
{
    public static GameManager Instance;

    public NetworkVariable<int> leftScore =
        new NetworkVariable<int>(0);

    public NetworkVariable<int> rightScore =
        new NetworkVariable<int>(0);

    public NetworkVariable<bool> gameOver =
        new NetworkVariable<bool>(false);

    public int winScore = 5;

    public Transform ball;
    public float ballSpeed = 6f;

    public TextMeshProUGUI leftScoreText;
    public TextMeshProUGUI rightScoreText;
    public TextMeshProUGUI winText;

    Rigidbody2D ballRb;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public override void OnNetworkSpawn()
    {
        if (ball != null)
            ballRb = ball.GetComponent<Rigidbody2D>();

        if (winText != null)
    winText.gameObject.SetActive(false);

        leftScore.OnValueChanged += UpdateScoreUI;
        rightScore.OnValueChanged += UpdateScoreUI;
    }

    void UpdateScoreUI(int oldValue, int newValue)
{
    if (leftScoreText != null)
        leftScoreText.text = leftScore.Value.ToString();

    if (rightScoreText != null)
        rightScoreText.text = rightScore.Value.ToString();
}

    void Update()
    {
        if (gameOver.Value)
        {
            winText.gameObject.SetActive(true);

            winText.text =
                leftScore.Value >= winScore ?
                "Left Player Wins!" :
                "Right Player Wins!";
        }
    }

    public void AddLeftScore()
    {
        if (!IsServer) return;

        leftScore.Value++;
        CheckWin();
        ResetBall(true);
    }

    public void AddRightScore()
    {
        if (!IsServer) return;

        rightScore.Value++;
        CheckWin();
        ResetBall(false);
    }

    void CheckWin()
    {
        if (leftScore.Value >= winScore ||
            rightScore.Value >= winScore)
        {
            gameOver.Value = true;
        }
    }

    void ResetBall(bool leftScored)
    {
        if (ball == null || ballRb == null) return;

        ball.position = Vector3.zero;
        ballRb.velocity = Vector2.zero;

        float direction = leftScored ? 1f : -1f;

        ballRb.velocity = new Vector2(
            direction * ballSpeed,
            Random.Range(-2f, 2f)
        );
    }

    public void StartGame()
    {
        if (!IsServer) return;

        leftScore.Value = 0;
        rightScore.Value = 0;
        gameOver.Value = false;

        ResetBall(true);
    }
}