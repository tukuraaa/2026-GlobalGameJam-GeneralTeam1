
using R3;

public class GameOverViewCtrl
{
    GameOverView _view;
    public GameOverViewCtrl(GameOverView view)
    {
        _view = view;
        init();
    }

    void init()
    {
        _view.OnRestart.SubscribeAwait(async(_, ct) =>
        {
            await MySceneManager.Instance.LoadSceneAsync("Game");
        }).AddTo(_view);
        
        _view.OnReturnToMenu.SubscribeAwait(async(_, ct) =>
        {
            await MySceneManager.Instance.LoadSceneAsync("Title");
            TitleManager.Instance.BackToMainMenu();
        }).AddTo(_view);

        _view.OnQuit.SubscribeAwait(async(_, ct) =>
        {
            //show warning, then quit.
            //for now, quit.
            await MySceneManager.Instance.QuitGame();
        }).AddTo(_view);

        _view.UpdateScore();

    }    
}
