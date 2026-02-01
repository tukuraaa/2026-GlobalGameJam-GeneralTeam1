using Cysharp.Threading.Tasks;
using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameView : MonoBehaviour
{

    [SerializeField]
    Button _resetButton;

    [SerializeField]
    EarthUnit earthUnit;

    [Space]
    [Header("player 1")]

    #region プレイヤー 1 スコアと力
    // [SerializeField]
    [SerializeField]
    TextMeshProUGUI playerOneScoreText;



    #endregion

    [Space]
    [Header("player 2")]

    #region プレイヤー 2 スコアと力
    [SerializeField]
    TextMeshProUGUI playerTwoScoreText;
    #endregion

    [Space]
    [Header("Earth HP")]


    [SerializeField]
    SimpleProgressBar EarthHPBar;

    private int lastSecond = 0;

    public float TimeInGame {get; private set;}

    public ReactiveProperty<int> playerOneScore = new ReactiveProperty<int>(0);
    private int playerOnePrevScore = 0;
    public ReactiveProperty<int> playerTwoScore = new ReactiveProperty<int>(0);
    private int playerTwoPrevScore = 0;


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
        EarthHPBar.Init(earthUnit.LifePoint.Value);
        new GameViewCtrl(this);
        TimeInGame = 0f;
        playerOneScore.Subscribe(PlayerOneScoreHandler).AddTo(this);
        // playerOneScore.SubscribeAwait<int>(PlayerOneScoreHandler).AddTo(this);
        playerTwoScore.Subscribe(PlayerTwoScoreHandler).AddTo(this);
        earthUnit.LifePoint.Subscribe(OnEarthDamage).AddTo(this);

    }

    private void OnEarthDamage(int hp)
    {
        EarthHPBar.currentValue.Value = hp;
    }

    private void PlayerOneScoreHandler(int score)
    {
        playerOneScoreText.text = $@"0{score}00";
    }

    private void PlayerTwoScoreHandler(int score)
    {
        playerTwoScoreText.text = $@"0{score}00";
    }

    void FixedUpdate()
    {
        TimeInGame += Time.fixedDeltaTime * DataConst.ScoreRate(Stage.Instance.NowLevel.CurrentValue);
        if(Mathf.RoundToInt(TimeInGame) - lastSecond >= 1)
        {
            playerOnePrevScore = playerOneScore.Value;
            playerOneScore.Value = Mathf.RoundToInt(TimeInGame);
            playerTwoPrevScore = playerTwoScore.Value;
            playerTwoScore.Value = Mathf.RoundToInt(TimeInGame);
        }
    }

    public void UpdateTimes(int times)
    {
        //just minus all their scores. 全部引くスコア。
        playerOnePrevScore = playerOneScore.Value;
        playerOneScore.Value -= Mathf.RoundToInt(DataConst.ScoreRate(Stage.Instance.NowLevel.CurrentValue));
        playerTwoPrevScore = playerTwoScore.Value;
        playerTwoScore.Value -= Mathf.RoundToInt(DataConst.ScoreRate(Stage.Instance.NowLevel.CurrentValue));


    }

    public void UpdateHp(int hp)
    {
        EarthHPBar.currentValue.Value = hp; 
        // _txtHp.text = $@"HP : {hp}";
    }    

}
