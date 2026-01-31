using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;

public class HighScoreViewCtrl
{
    HighScoreView _view;
    public HighScoreViewCtrl(HighScoreView view)
    {
        _view = view;
        init();
    }

    void init()
    {
        _view.OnMainMenu.SubscribeAwait(async (_, ct)=>
        {
            TitleManager.Instance.BackToMainMenu();
            await UniTask.NextFrame();
        }).AddTo(_view);
    }    
}
