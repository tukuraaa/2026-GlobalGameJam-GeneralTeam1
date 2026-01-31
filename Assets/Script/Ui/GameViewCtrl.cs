using R3;
using Cysharp.Threading.Tasks;
using System.Threading.Tasks;
using System.Threading;
using ObservableCollections;
using UnityEngine;

public class GameViewCtrl
{
    GameView _view;
    int _hitTimes = 0;
    public GameViewCtrl(GameView view)
    {
        _view = view;

        init();
    }

    void init()
    {
        var stage = Stage.Instance;

        _view.ResetButtonClicked.Subscribe((_) =>
        {
            onReset().Forget();
        }).AddTo(_view);

        stage.EarthUnit.LifePoint.SubscribeAwait(OnHpChangedHandler).AddTo(_view);

        foreach (var shooter in stage.Shooters)
        {
            shooter.OnHit().Subscribe(
                (hitMsg) =>
                {
                    if(hitMsg.HitObj.gameObject.GetInstanceID() == stage.EarthUnit.gameObject.GetInstanceID())
                    {
                        _hitTimes++;
                        _view.UpdateTimes(_hitTimes);
                    }
                }
            ).AddTo(_view);
        }
    }

    private async ValueTask OnHpChangedHandler(int x, CancellationToken token)
    {
        if(x > 0)
        {
            _view.UpdateHp(x);
        }
        else
        {
            await MySceneManager.Instance.LoadSceneAsync("Title");
            TitleManager.Instance.GameOver(Mathf.RoundToInt(_view.TimeInGame));
            //now that you've loaded, show the gameover view.

        }
    }


    async UniTaskVoid onReset()
    {
        MySceneManager.Instance.LoadSceneAsync("Game").Forget();
        await UniTask.NextFrame();
    }
}