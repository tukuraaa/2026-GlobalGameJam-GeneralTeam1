using System;
using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameView : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI _txtHitTimes;
    [SerializeField]
    TextMeshProUGUI _txtHp;

    [SerializeField]
    TextMeshProUGUI _txtScore;

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

    void Update()
    {
        TimeInGame += Time.deltaTime;
        if(Mathf.RoundToInt(TimeInGame) - lastSecond >= 1)
        {
            lastSecond += 1;
            UpdateScore(Mathf.RoundToInt(TimeInGame));
        }
    }

    public void UpdateTimes(int times)
    {
        _txtHitTimes.text = $@"HitTimes : {times}";
    }

    public void UpdateHp(int hp)
    {
        _txtHp.text = $@"HP : {hp}";
    }    

    public void UpdateScore(int score)
    {
        _txtScore.text = $@"Score : {score}";
        
    }
}
