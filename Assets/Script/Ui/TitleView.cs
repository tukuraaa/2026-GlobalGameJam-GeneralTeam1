using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;
using UnityEngine.UI;

public class TitleView : MonoBehaviour
{
    [SerializeField]
    Button _startButton;
    public Observable<Unit> OnStart => _startButton.OnClickAsObservable();

    void Start ()
    {
        new TitleViewCtrl(this);
    }
}