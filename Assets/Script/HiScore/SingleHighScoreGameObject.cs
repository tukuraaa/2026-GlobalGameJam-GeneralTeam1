using TMPro;
using UnityEngine;

public class SingleHighScoreGameObject : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI nameText;

    [SerializeField]
    TextMeshProUGUI scoreText;

    public void Initialize(string name, int score)
    {
        nameText.text = name;
        scoreText.text = $"0{score}00";
    }
}
