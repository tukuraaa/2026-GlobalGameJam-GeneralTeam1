using Cysharp.Threading.Tasks;
using R3;
using UnityEditor;

public class TitleViewCtrl
{
    TitleView _view;
    public TitleViewCtrl(TitleView view)
    {
        _view = view;
        init();
    }

    void init()
    {
        _view.OnStart.SubscribeAwait(async (_, ct) =>
        {
            await MySceneManager.Instance.LoadSceneAsync("Game");
        }).AddTo(_view);

        _view.OnHighScore.SubscribeAwait(async(_, ct) =>
        {
            await UniTask.WaitForEndOfFrame();
        }).AddTo(_view);

        _view.OnQuit.SubscribeAwait(async(_, ct) =>
        {
            //show warning, then quit.
            //for now, quit.
            await MySceneManager.Instance.QuitGame();
        }).AddTo(_view);

    }    
}
