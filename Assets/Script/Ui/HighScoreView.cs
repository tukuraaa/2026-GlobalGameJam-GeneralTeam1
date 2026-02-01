using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreView : MonoBehaviour
{
    [SerializeField]
    ScrollRect highScoreScroll;
    
    [SerializeField]
    Button mainMenuButton;

    [SerializeField]
    SingleHighScoreGameObject prefab;

    private HighScoresObject highScores;

    public Observable<Unit> OnMainMenu => mainMenuButton.OnClickAsObservable();

    void OnValidate()
    {
        if(mainMenuButton == null)
        {
            mainMenuButton = transform.Find("MainMenuButton").GetComponent<Button>();
        }
        if(highScoreScroll == null)
        {
            highScoreScroll = transform.Find("HighScoreScroll").GetComponent<ScrollRect>();
        }

    }
    void Start()
    {
        SingleHighScoreGameObject temp;
        highScores = HighScoresObject.LoadHighScore();
        if(highScores != null)
        {
            Debug.Log($"number of high scores: {highScores.highScores.Count}");
            highScores.highScores.Sort();
            foreach (SingleHighScore highscore in highScores.highScores)
            {
                Debug.Log(highscore);
                
                temp = Instantiate(prefab);
                temp.Initialize(highscore.name, highscore.score);
                temp.transform.SetParent(highScoreScroll.content);
            }
        }
        else
        {
            Debug.Log($"やば。");
            HighScoresObject.TryCreateDataFile();
            highScores = HighScoresObject.LoadHighScore();
        }
        new HighScoreViewCtrl(this);
    }

}
