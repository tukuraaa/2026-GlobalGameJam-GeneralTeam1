using System;
using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverView : MonoBehaviour
{
    [SerializeField]
    TMP_InputField nameInput;


    [SerializeField]
    TextMeshProUGUI scoreText;

    [SerializeField]
    Button restartButton;

    [SerializeField]
    Button returnToMenuButton;

    [SerializeField]
    Button quitButton;
    
    // public Observable<string> OnNameInput => nameInput.OnValueChangedAsObservable();
    // public Observable<int> OnGameOver = new Observable<int>()
    public Observable<Unit> OnRestart => restartButton.OnClickAsObservable();

    public Observable<Unit> OnReturnToMenu => returnToMenuButton.OnClickAsObservable();

    public Observable<Unit> OnQuit => quitButton.OnClickAsObservable();

    void OnValidate()
    {
        if(nameInput == null)
        {
            nameInput = transform.Find("NameInput").GetComponent<TMP_InputField>();
        }
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

    void OnDisable()    //やば… Just add to the highscore when player closes.
    {
        Debug.Log(nameInput.text);
        HighScoresObject.TryUpdateDataFile(new(nameInput.text, PlayerPrefs.GetInt("Score")));
        PlayerPrefs.DeleteKey("Score"); //reset
        nameInput.text = "";
    }
    public void UpdateScore()
    {
        scoreText.text = $"Score: 0{PlayerPrefs.GetInt("Score")}00";
    }
}
