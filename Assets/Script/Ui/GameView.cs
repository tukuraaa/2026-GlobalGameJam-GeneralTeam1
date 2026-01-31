using System;
using R3;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameView : MonoBehaviour
{
    
    [SerializeField]
    TextMeshProUGUI _txtHp;

    [SerializeField]
    TextMeshProUGUI playerOneScoreText;

    [SerializeField]
    [FormerlySerializedAs("_txtHitTimes")]
    TextMeshProUGUI playerTwoScoreText;

    [SerializeField]
    Button _resetButton;

    public float TimeInGame {get; private set;}
    public int lastSecond = 0;

    public Observable<Unit> ResetButtonClicked => _resetButton.OnClickAsObservable();
    

// public Observable a = Observable.Interval(TimeSpan.FromSeconds(1))
//     .Select((_, i) => i)
//     .Where(x => x % 2 == 0)
//     .Subscribe(x => Console.WriteLine($"Interval:{x}"));

    void Start()
    {
        new GameViewCtrl(this);
        TimeInGame = 0f;
    }

    void FixedUpdate()
    {
        TimeInGame += (Time.fixedDeltaTime) * DataConst.ScoreRate(Stage.Instance.NowLevel.CurrentValue);
        if(Mathf.RoundToInt(TimeInGame) - lastSecond >= 1)
        {
            lastSecond += 1;
            UpdateScore(Mathf.RoundToInt(TimeInGame));
        }
    }

    public void UpdateTimes(int times)
    {
        playerTwoScoreText.text = $@"HitTimes : {times}";
    }

    public void UpdateHp(int hp)
    {
        _txtHp.text = $@"HP : {hp}";
    }    

    public void UpdateScore(int score)
    {
        playerOneScoreText.text = $@"Score : {score}";
        
    }
}
