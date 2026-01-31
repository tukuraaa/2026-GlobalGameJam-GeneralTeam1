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
        _view.OnMainMenu.Subscribe((_)=>
        {
            TitleManager.Instance.BackToMainMenu();
        }).AddTo(_view);
    }    
}
