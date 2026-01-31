using UnityEngine;

public class TitleManager : SingletonClass<TitleManager>
{
    [SerializeField]
    private TitleView titleView;
    
    [SerializeField]
    private HighScoreView highScoreView;

    [SerializeField]
    private GameOverView gameOverView;

    public void BackToMainMenu()
    {
        titleView.gameObject.SetActive(true);
        highScoreView.gameObject.SetActive(false);
        gameOverView.gameObject.SetActive(false);
    }
}