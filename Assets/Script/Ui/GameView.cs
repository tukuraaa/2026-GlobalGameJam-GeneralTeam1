using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameView : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI _txtHitTimes;
    [SerializeField]
    TextMeshProUGUI _txtHp;

    void Start()
    {
        new GameViewCtrl(this);
    }

    public void UpdateTimes(int times)
    {
        _txtHitTimes.text = $@"HitTimes : {times}";
    }

    public void UpdateHp(int hp)
    {
        _txtHp.text = $@"HP : {hp}";
    }    
}
