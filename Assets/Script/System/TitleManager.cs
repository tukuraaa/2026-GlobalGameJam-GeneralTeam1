using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class TitleManager : Singleton<TitleManager>
{
    //GUH FIX LATER.
    // public static TitleManager Instance => MySceneManager.Instance.NowSceneName.Equals("Title") 
    // ? FindFirstObjectByType<TitleManager>() : null;
    
    [SerializeField]
    private TitleView titleView;
    
    [SerializeField]
    private HighScoreView highScoreView;

    [SerializeField]
    private GameOverView gameOverView;

    void OnValidate()
    {
        if(titleView == null)
        {
            titleView = transform.Find("TitleView").GetComponent<TitleView>();
        }
        if(highScoreView == null)
        {
            highScoreView = transform.Find("HighScoreView").GetComponent<HighScoreView>();
        }
        if(gameOverView == null)
        {
            gameOverView = transform.Find("GameOverView").GetComponent<GameOverView>();
        }
    }

    public void BackToMainMenu()
    {
        titleView.gameObject.SetActive(true);
        highScoreView.gameObject.SetActive(false);
        gameOverView.gameObject.SetActive(false);
    }

    public async Task AsyncBackToMainMenu()
    {
        Debug.Log("HOEH.");
        titleView.gameObject.SetActive(true);
        gameOverView.gameObject.SetActive(false);
        await UniTask.NextFrame();
        highScoreView.gameObject.SetActive(false);

    }

    public void ShowHighScore()
    {
        titleView.gameObject.SetActive(false);
        highScoreView.gameObject.SetActive(true);
        gameOverView.gameObject.SetActive(false);
    }

    public void GameOver(int score = 0)
    {
        titleView.gameObject.SetActive(false);
        highScoreView.gameObject.SetActive(false);
        gameOverView.gameObject.SetActive(true);
        //bruh spaghetti? PlayerPrefs?
        PlayerPrefs.SetInt("Score", score);
    }


}