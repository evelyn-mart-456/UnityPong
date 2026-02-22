using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    public TextMeshProUGUI leftScoreText;
    public TextMeshProUGUI rightScoreText;

    void Update()
    {
        if (GameManager.Instance == null) return;

        leftScoreText.text =
            GameManager.Instance.leftScore.Value.ToString();

        rightScoreText.text =
            GameManager.Instance.rightScore.Value.ToString();
    }
}
