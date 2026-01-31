using UnityEngine;
using R3;
using Cysharp.Threading.Tasks;

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

        stage.EarthUnit.LifePoint.Subscribe(x =>
        {
            _view.UpdateHp(x);
        }).AddTo(_view);

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

    async UniTaskVoid onReset()
    {
        MySceneManager.Instance.LoadSceneAsync("Game").Forget();
    }
}