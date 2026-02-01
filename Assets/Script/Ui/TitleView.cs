using System;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;
using UnityEngine.UI;

public class TitleView : MonoBehaviour
{
    [SerializeField]
    Button _startButton;

    [SerializeField]
    Button _highScoreButton;

    [SerializeField]
    Button _quitButton;
    [SerializeField]
    AudioClip _titleBgm = null;
    public Observable<Unit> OnStart => _startButton.OnClickAsObservable();
    public Observable<Unit> OnHighScore => _highScoreButton.OnClickAsObservable();
    public Observable<Unit> OnQuit => _quitButton.OnClickAsObservable();
    void OnValidate()
    {
        if(_startButton == null)
        {
            _startButton = transform.Find("StartButton").GetComponent<Button>();
        }

        if(_highScoreButton == null)
        {
            _highScoreButton = transform.Find("HighScoreButton").GetComponent<Button>();
        }
        if(_quitButton == null)
        {
            _quitButton = transform.Find("QuitButton").GetComponent<Button>();
        }
    }

    void Start ()
    {
        new TitleViewCtrl(this);

        AudioManager.Instance.PlayBgm(_titleBgm);
    }
}