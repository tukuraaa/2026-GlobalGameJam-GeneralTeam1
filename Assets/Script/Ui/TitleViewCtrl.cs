using R3;

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
    }    
}
