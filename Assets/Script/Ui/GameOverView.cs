using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverView : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI scoreText;

    [SerializeField]
    Button restartButton;

    [SerializeField]
    Button returnToMenuButton;

    [SerializeField]
    Button quitButton;
    

    // public Observable<int> OnGameOver = new Observable<int>()
    public Observable<Unit> OnRestart => restartButton.OnClickAsObservable();

    public Observable<Unit> OnReturnToMenu => returnToMenuButton.OnClickAsObservable();

    public Observable<Unit> OnQuit => quitButton.OnClickAsObservable();

    void OnValidate()
    {
        if(scoreText == null)
        {
            scoreText = transform.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        }
        if(restartButton == null)
        {
            restartButton = transform.Find("RestartButton").GetComponent<Button>();
        }
        if(returnToMenuButton == null)
        {
            returnToMenuButton = transform.Find("ReturnToMenuButton").GetComponent<Button>();
        }

        if(quitButton == null)
        {
            quitButton = transform.Find("QuitButton").GetComponent<Button>();
        }
    }

    void Start()
    {
        new GameOverViewCtrl(this);
    }


    public void UpdateScore()
    {
        scoreText.text = $"Score: {PlayerPrefs.GetInt("Score")}";
        HighScoresObject.TryUpdateDataFile(new("AAA", PlayerPrefs.GetInt("Score")));
        PlayerPrefs.DeleteKey("Score"); //reset
    }
}
