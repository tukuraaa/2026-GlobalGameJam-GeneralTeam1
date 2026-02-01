using System;
using R3;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameView : MonoBehaviour
{
    


    [SerializeField]
    TextMeshProUGUI playerOneScoreText;

    [SerializeField]
    [FormerlySerializedAs("_txtHitTimes")]
    TextMeshProUGUI playerTwoScoreText;

    [SerializeField]
    Button _resetButton;

    [SerializeField]
    EarthUnit earthUnit;

    [Space]
    [Header("player 1")]

    #region プレイヤー 1 スコアと力
    // [SerializeField]




    #endregion

    #region プレイヤー 2 スコアと力

    #endregion

    [Space]
    [Header("Earth HP")]


    [SerializeField]
    SimpleProgressBar EarthHPBar;

    private int lastSecond = 0;

    public float TimeInGame {get; private set;}

    public Observable<Unit> ResetButtonClicked => _resetButton.OnClickAsObservable();

    void OnValidate()
    {
        if(earthUnit == null)
        {
            earthUnit = FindFirstObjectByType<EarthUnit>(); //多分一つだけ、大丈夫
        }
    }

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
        EarthHPBar.currentValue.Value = hp; 
        // _txtHp.text = $@"HP : {hp}";
    }    

    public void UpdateScore(int score)
    {
        playerOneScoreText.text = $@"Score : {score}";
        
    }
}
